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

        private void Start()
        {
            playerRigidBody2D = GetComponent<Rigidbody2D>();
            playerCollider2D = GetComponent<BoxCollider2D>();
            
        }

        private void Update()
        {
            rigidbodyVelocity = playerRigidBody2D.velocity;
            GetInput();
            CheckGrounded();
            CalculateHorizontalSpeed();
            CalculateJumpVelocity();
            MovePlayer();
        }

        #region INPUT
        
        private float xInput;
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

        [SerializeField] private bool isGrounded;
        [SerializeField] private int count;

        private void CheckGrounded()
        {
            count = 0;
            for (int i = 0; i < rayCount; i++)
            {
                var _positionNow = transform.position;
                var _raySpacing = ((playerCollider2D.bounds.size.x - 2*skinWidth) / (rayCount - 1));
                var _rayOrigin = (_positionNow - new Vector3(0.5f - skinWidth, 0.5f - skinWidth, 0)) + (Vector3.right * (_raySpacing * i));
                var _raycastHit2D = Physics2D.Raycast(_rayOrigin, Vector2.down, rayLength, layerMask);


                if (_raycastHit2D.collider == null)
                {
                    Debug.DrawRay(_rayOrigin, Vector2.down * rayLength, Color.green);
                    
                }
                else
                {
                    Debug.DrawRay(_rayOrigin, Vector2.down * rayLength, Color.red);
                    count++;
                }
            }

            if (count > 0 && count <= rayCount)
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

        

        #endregion
        
        #region HORIZONATL MOVEMENT
        
        [Header("HORIZONTAL MOVEMENT")]
        [SerializeField] private float acceleration = 90f;
        [SerializeField] private float deAcceleration = 90f;
        [SerializeField] private float maxSpeed = 11f;
        [SerializeField] private float currentHorizontalSpeed = 0;
        [SerializeField] private float currentVerticalSpeed = 0;
        private void CalculateHorizontalSpeed()
        {
            currentHorizontalSpeed = xInput != 0
                ? Mathf.MoveTowards(currentHorizontalSpeed, maxSpeed * xInput, acceleration * Time.deltaTime)
                : Mathf.MoveTowards(currentHorizontalSpeed, 0, deAcceleration * Time.deltaTime);
        }
        #endregion

        #region JUMPING

        [SerializeField] private AnimationCurve jumpVelocityCurve;
        private bool startJump;
        [FormerlySerializedAs("_jumpTime")] [SerializeField] private float jumpTime = 0;
        private void CalculateJumpVelocity()
        {
            
            currentVerticalSpeed = rigidbodyVelocity.y;
            
            if (isJumpDown)
            {
                startJump = true;
                jumpTime = 0;
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
