using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Rigidbody2D rigiBody2D;
    [SerializeField] Collider2D boxCollider2D;

    private void Start()
    {
        rigiBody2D = GetComponent<Rigidbody2D>();
        boxCollider2D = GetComponent<BoxCollider2D>();
        Physics2D.queriesStartInColliders = false;
    }

    private void Update()
    {
        GetInput();
        SetHorizonatlSpeed();
        CheckGrounded();
        CalculateGravity();
        CalculateJump();
    }

    #region INPUT
    [Header("INPUT")]
    [SerializeField] private float inputX;
    [SerializeField] private float inputY;
    [SerializeField] private bool jumpDown;
    [SerializeField] private bool jumpUp;
    private void GetInput()
    {
        inputX = Input.GetAxisRaw("Horizontal");
        inputY = Input.GetAxisRaw("Vertical");

        jumpDown = Input.GetKeyDown(KeyCode.Space);
        jumpUp = Input.GetKeyUp(KeyCode.Space);
    }
    #endregion

    #region HORIZONTAL MOVEMENT
    [Header("MOVEMENT")]
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deacceleration = 50f;
    [SerializeField] private float currentHorizontalSpeed;
    [SerializeField] private float currentVerticalSpeed;

    void SetHorizonatlSpeed()
    {
        if (inputX!=0)
        {
            currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, movementSpeed * inputX, acceleration * Time.deltaTime);
        }
        else
        {
            currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, 0, deacceleration * Time.deltaTime);
        }
        rigiBody2D.velocity = new Vector2(currentHorizontalSpeed, rigiBody2D.velocity.y);
    }
    #endregion

    #region GROUND CHECK
    [Header("GROUND CHECK")]
    [SerializeField] private bool isGrounded = false;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private float rayLength = 1f;
    void CheckGrounded()
    {
        Vector3 _rayOrigin = boxCollider2D.bounds.center;
        Vector2 _debugRayDirection = Vector2.down * rayLength;
        Color _rayColor;
        RaycastHit2D _raycastHit2D = Physics2D.Raycast(_rayOrigin, Vector2.down, rayLength, layerMask);
        _rayColor = (_raycastHit2D.collider != null) ? Color.red : Color.green;

        isGrounded = (_raycastHit2D.collider != null);
        Debug.DrawRay(_rayOrigin, _debugRayDirection, _rayColor);
        Debug.Log($"The colider is : {_raycastHit2D.collider}");
    }
    #endregion

    #region CALCULATING GRAVITY
    [Header("GRAVITY")]
    [SerializeField] private float defaultGravity = 1f;
    [SerializeField][Range(1,50)] private float gravityUp = 15f;
    [SerializeField][Range(1,50)] private float gravityDown = 20f;
    void CalculateGravity()
    {
        if (rigiBody2D.velocity.y > 0)
        { 
            rigiBody2D.gravityScale = gravityUp;
        }
        else if (rigiBody2D.velocity.y<0)
        {
            rigiBody2D.gravityScale = gravityDown;
        }
        else
        {
            rigiBody2D.gravityScale = defaultGravity;
        }

    }
    #endregion

    #region JUMPING
    [Header("JUMPING")]
    [SerializeField] private float jumpHeight = 40f;
    void CalculateJump()
    {
        if (jumpDown && isGrounded)
        {
            rigiBody2D.velocity = new Vector2(rigiBody2D.velocity.x, jumpHeight);
        }
        if (jumpUp && !isGrounded && rigiBody2D.velocity.y>0)
        {
            rigiBody2D.velocity = new Vector2(rigiBody2D.velocity.x, 0);
        }
    }
    #endregion
}
