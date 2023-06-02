
using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Movement
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D playerRigidBody2D;
        [SerializeField] private BoxCollider2D playerCollider2D;
        [SerializeField] private Vector2 rigidbodyVelocity;

        public event Action OnPlayerLand;
        public event Action OnPlayerJump; 
        private void Start()
        {
            playerRigidBody2D = GetComponent<Rigidbody2D>();
            playerCollider2D = GetComponent<BoxCollider2D>();
            
        }

        private void Update()
        {
            rigidbodyVelocity = playerRigidBody2D.velocity;
            
            // this checks of teh player has just landed and fires the corresponding event.
            
            
            GetInput();
            CheckGrounded();
            CheckCeiling();
            CalculateHorizontalSpeed();
            CalculateJumpVelocity();
            MovePlayer();

            
            
        }

        #region INPUT
        
        public float XInput { get; private set; }
        private bool isJumpDown;
        private bool isJumpUp;

        private void GetInput()
        {
            XInput = Input.GetAxisRaw("Horizontal");
            isJumpUp = Input.GetKeyUp(KeyCode.Space);
            isJumpDown = Input.GetKeyDown(KeyCode.Space);
        }
        #endregion

        #region Ground Check

        [Header("GROUND CHECK")] 
        [SerializeField] private int rayCount = 3;
        [SerializeField] private float rayLength = 0.3f;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float skinWidth = 0.1f;
        
        public bool IsGrounded { get; private set; }
        [FormerlySerializedAs("count")] [SerializeField] private int countForGround;

        private void CheckGrounded()
        {
            countForGround = 0;
            for (var _i = 0; _i < rayCount; _i++)
            {
                var _positionNow = transform.position;
                var _bounds = playerCollider2D.bounds;
                var _height = _bounds.extents.y;
                var _raySpacing = ((_bounds.size.x - 2 * skinWidth ) / (rayCount-1));
                var _rayOrigin = (_positionNow - new Vector3(_bounds.extents.x - skinWidth, _height - skinWidth, 0)) + (Vector3.right * (_raySpacing * _i));
                var _raycastHit2D = Physics2D.Raycast(_rayOrigin, Vector2.down, rayLength, layerMask);


                if (_raycastHit2D.collider == null)
                {
                    Debug.DrawRay(_rayOrigin, Vector2.down * rayLength, Color.green);
                    
                }
                else
                {
                    Debug.DrawRay(_rayOrigin, Vector2.down * rayLength, Color.red);
                    countForGround++;
                }
            }

            if (countForGround > 0 && countForCeiled <= rayCount)
            {
                IsGrounded = true;
            }
            else
            {
                IsGrounded = false;
            }
        }
        #endregion

        #region CEILING CHECK

        [SerializeField] private bool isCeiled;
        [SerializeField] private int countForCeiled;

        private void CheckCeiling()
        {
            countForCeiled = 0;
            for (var _i = 0; _i < rayCount; _i++)
            {
                var _positionNow = transform.position;
                var _bounds = playerCollider2D.bounds;
                var _height = _bounds.extents.y;
                var _raySpacing = ((_bounds.size.x - 2*skinWidth) / (rayCount - 1));
                var _rayOrigin = (_positionNow + new Vector3(_bounds.extents.x - skinWidth, _height - skinWidth, 0)) + (Vector3.right * (-_raySpacing * _i));
                var _raycastHit2D = Physics2D.Raycast(_rayOrigin, Vector2.up, rayLength, layerMask);


                if (_raycastHit2D.collider == null)
                {
                    Debug.DrawRay(_rayOrigin, Vector2.up * rayLength, Color.green);
                    
                }
                else
                {
                    Debug.DrawRay(_rayOrigin, Vector2.up * rayLength, Color.red);
                    countForCeiled++;
                }
            }

            if (countForCeiled > 0 && countForCeiled <= rayCount)
            {
                isCeiled = true;
            }
            else
            {
                isCeiled = false;
            }
        }

        #endregion
        
        #region HORIZONATL MOVEMENT
        
        [Header("HORIZONTAL MOVEMENT")]
        [SerializeField] private float acceleration = 90f;
        [SerializeField] private float deAcceleration = 90f;
        [SerializeField] private float maxSpeed = 11f;
        public float CurrentHorizontalSpeed { get; private set; }
        public float CurrentVerticalSpeed { get; private set; }
        private void CalculateHorizontalSpeed()
        {
            CurrentHorizontalSpeed = XInput != 0
                ? Mathf.MoveTowards(CurrentHorizontalSpeed, maxSpeed * XInput, acceleration * Time.deltaTime)
                : Mathf.MoveTowards(CurrentHorizontalSpeed, 0, deAcceleration * Time.deltaTime);
        }
        #endregion

        #region JUMPING
        [Header("JUMPING")]
        [SerializeField] private AnimationCurve jumpVelocityCurve;
        public bool Jumping { get; private set; }
        [FormerlySerializedAs("_jumpTime")] [SerializeField] private float jumpTime;
        [SerializeField] private bool isJumpBuffered;
        [SerializeField] private bool isInCayoteeTime;
        [SerializeField] private float jumpBufferTime = 0.3f;
        [SerializeField] private float cayoteeTime = 0.3f;
        [SerializeField]private float bufferTimer;
        private bool startBufferTimer;
        [SerializeField]private float cayoteeTimer ;
       
        
        
        

        private void CalculateJumpVelocity()
        {
            
            CurrentVerticalSpeed = rigidbodyVelocity.y;
            
            CalculateJumpBuffer();
            CalculateCayoteeTime();
            
            
            if (IsGrounded || (isInCayoteeTime))
            {
               
                if (isJumpDown || (isJumpBuffered && IsGrounded))
                {
                    OnPlayerJump?.Invoke();
                    isJumpBuffered = false;
                    isInCayoteeTime = false;
                    Jumping = true;
                    jumpTime = 0;

                    

                }
            }
            
            if (Jumping)
            {
                jumpTime += Time.deltaTime;
                CurrentVerticalSpeed = jumpVelocityCurve.Evaluate(jumpTime);
                
            }

            if (jumpTime > 0.5f && Jumping)
            {
                CurrentVerticalSpeed = 0;
                Jumping = false;
            }
            
            if (isJumpUp && !(rigidbodyVelocity.y <= 0))
            {
                CurrentVerticalSpeed = 0;
                Jumping = false;
            }

            if (isCeiled)
            {
                // apply some downwards velocity to repel teh player from the ceiling.
                CurrentVerticalSpeed = -2;
                Jumping = false;
            }
        }
        
        
        private void CalculateJumpBuffer()
        {
            
            if (isJumpDown && !IsGrounded)
            {
                startBufferTimer = true;
            }

            if (startBufferTimer)
            {
                bufferTimer += Time.deltaTime;
                isJumpBuffered = bufferTimer < jumpBufferTime;
            }
            if (isJumpUp)
            {
                startBufferTimer = false;
                isJumpBuffered = false;
            }

            if (IsGrounded)
            {
                startBufferTimer = false;
                bufferTimer = 0;
            }
        }
        
        
        private void CalculateCayoteeTime()
        {
            
            if (!IsGrounded )
            {
                cayoteeTimer += Time.deltaTime;
                isInCayoteeTime = cayoteeTimer < cayoteeTime;
            }
            else if(cayoteeTimer > 0 && IsGrounded)
            {
                OnPlayerLand?.Invoke();
                cayoteeTimer = 0;
            }
            else
            {
                cayoteeTimer = 0;
            }
        }
        #endregion

        #region SETTING VELOCITY

        private void MovePlayer()
        {
            playerRigidBody2D.velocity = new Vector2(CurrentHorizontalSpeed, CurrentVerticalSpeed);
        }
        #endregion
    }
    
}
