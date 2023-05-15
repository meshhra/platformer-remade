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
        [Header("JUMPING")]
        [SerializeField] private float jumpVelocity = 10f;
        private void CalculateJumpVelocity()
        {
            currentVerticalSpeed = rigidbodyVelocity.y;
            if (isJumpUp) currentVerticalSpeed = 0; 
            
            if (!isJumpDown) return;
            currentVerticalSpeed = jumpVelocity;
            
            

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
