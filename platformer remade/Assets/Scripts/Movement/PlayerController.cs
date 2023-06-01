
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
        
        [SerializeField]private float xInput;
        private bool isJumpDown;
        private bool isJumpUp;

        private void GetInput()
        {
            xInput = Input.GetAxisRaw("Horizontal");
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
        
        [FormerlySerializedAs("xDelta")] [SerializeField] private float firstRayPosX = 0.935f;
        [SerializeField] private bool isGrounded;
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
                isGrounded = true;
            }
            else
            {
                isGrounded = false;
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
        [SerializeField] private float currentHorizontalSpeed ;
        [SerializeField] private float currentVerticalSpeed ;
        private void CalculateHorizontalSpeed()
        {
            currentHorizontalSpeed = xInput != 0
                ? Mathf.MoveTowards(currentHorizontalSpeed, maxSpeed * xInput, acceleration * Time.deltaTime)
                : Mathf.MoveTowards(currentHorizontalSpeed, 0, deAcceleration * Time.deltaTime);
        }
        #endregion

        #region JUMPING
        [Header("JUMPING")]
        [SerializeField] private AnimationCurve jumpVelocityCurve;
        private bool startJump;
        [FormerlySerializedAs("_jumpTime")] [SerializeField] private float jumpTime;
        [SerializeField] private bool isJumpBuffered;
        [SerializeField] private bool isInCayoteeTime;
        [SerializeField] private float jumpBufferTime = 0.3f;
        [SerializeField] private float cayoteeTime = 0.3f;
        [SerializeField]private float bufferTimer;
        private bool startBufferTimer;
        [SerializeField]private float cayoteeTimer ;
       
        
        [FormerlySerializedAs("isPlayerJustLanded")] [SerializeField] private bool isLanded;
        

        private void CalculateJumpVelocity()
        {
            
            currentVerticalSpeed = rigidbodyVelocity.y;
            
            CalculateJumpBuffer();
            CalculateCayoteeTime();
            
            
            if (isGrounded || (isInCayoteeTime))
            {
               
                if (isJumpDown || (isJumpBuffered && isGrounded))
                {
                    OnPlayerJump?.Invoke();
                    isJumpBuffered = false;
                    isInCayoteeTime = false;
                    startJump = true;
                    jumpTime = 0;

                    

                }
            }
            
            if (startJump)
            {
                jumpTime += Time.deltaTime;
                currentVerticalSpeed = jumpVelocityCurve.Evaluate(jumpTime);
                
            }

            if (jumpTime > 0.5f && startJump)
            {
                currentVerticalSpeed = 0;
                startJump = false;
            }
            
            if (isJumpUp && !(rigidbodyVelocity.y <= 0))
            {
                currentVerticalSpeed = 0;
                startJump = false;
            }

            if (isCeiled)
            {
                // apply some downwards velocity to repel teh player from the ceiling.
                currentVerticalSpeed = -2;
                startJump = false;
            }
        }
        
        
        private void CalculateJumpBuffer()
        {
            
            if (isJumpDown && !isGrounded)
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

            if (isGrounded)
            {
                startBufferTimer = false;
                bufferTimer = 0;
            }
        }
        
        
        private void CalculateCayoteeTime()
        {
            
            if (!isGrounded )
            {
                cayoteeTimer += Time.deltaTime;
                isInCayoteeTime = cayoteeTimer < cayoteeTime;
            }
            else if(cayoteeTimer > 0 && isGrounded)
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
            playerRigidBody2D.velocity = new Vector2(currentHorizontalSpeed, currentVerticalSpeed);
        }
        #endregion
    }
    
}
