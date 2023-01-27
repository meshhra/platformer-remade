
using System;
using UnityEngine;
using static UnityEngine.UI.Image;

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

    [SerializeField] private Vector2 velocity;
    private void Update()
    {
        velocity = rigiBody2D.velocity;
        GetInput();
        //SetHorizonatlSpeed();
        CheckGrounded();
        CheckCieling();
        CalculateGravity();
        CalculateJump();
    }

    private void FixedUpdate()
    {
        SetHorizonatlSpeed();
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
        if (inputX != 0)
        {
            //currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, movementSpeed * inputX, acceleration * Time.deltaTime);
            currentHorizontalSpeed += Time.deltaTime * movementSpeed * acceleration * inputX;
            currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed, -movementSpeed, movementSpeed);
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
    [SerializeField] private float raySpacing;
    [SerializeField] private int rayCount = 4;

    void CheckGrounded()
    {
        raySpacing = boxCollider2D.bounds.size.x / (rayCount - 1);
        Vector2 _rayOrigin0 = boxCollider2D.bounds.min + (Vector3.right * raySpacing * 0);
        Vector2 _rayOrigin1 = boxCollider2D.bounds.min + (Vector3.right * raySpacing * 1);
        Vector2 _rayOrigin2 = boxCollider2D.bounds.min + (Vector3.right * raySpacing * 2);
        Vector2 _rayOrigin3 = boxCollider2D.bounds.min + (Vector3.right * raySpacing * 3);


        RaycastHit2D hit0 = Physics2D.Raycast(_rayOrigin0, Vector2.down, rayLength, layerMask);
        RaycastHit2D hit1 = Physics2D.Raycast(_rayOrigin1, Vector2.down, rayLength, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(_rayOrigin2, Vector2.down, rayLength, layerMask);
        RaycastHit2D hit3 = Physics2D.Raycast(_rayOrigin3, Vector2.down, rayLength, layerMask);
        if (hit0.collider == null && hit1.collider == null && hit2.collider == null && hit3.collider == null)
        {
            Debug.DrawRay(_rayOrigin0, Vector2.down * rayLength, Color.green);
            Debug.DrawRay(_rayOrigin1, Vector2.down * rayLength, Color.green);
            Debug.DrawRay(_rayOrigin2, Vector2.down * rayLength, Color.green);
            Debug.DrawRay(_rayOrigin3, Vector2.down * rayLength, Color.green);
            isGrounded = false;
        }
        else
        {
            Debug.DrawRay(_rayOrigin0, Vector2.down * rayLength, Color.red);
            Debug.DrawRay(_rayOrigin1, Vector2.down * rayLength, Color.red);
            Debug.DrawRay(_rayOrigin2, Vector2.down * rayLength, Color.red);
            Debug.DrawRay(_rayOrigin3, Vector2.down * rayLength, Color.red);
            isGrounded = true;
        }

    }
    #endregion

    #region CIELING CHECK
    [Header("CIELING CHECK")]
    [SerializeField] private bool isCieled = false;


    void CheckCieling()
    {
        raySpacing = boxCollider2D.bounds.size.x / (rayCount - 1);
        Vector2 _rayOrigin0 = boxCollider2D.bounds.max + (Vector3.right * -raySpacing * 0);
        Vector2 _rayOrigin1 = boxCollider2D.bounds.max + (Vector3.right * -raySpacing * 1);
        Vector2 _rayOrigin2 = boxCollider2D.bounds.max + (Vector3.right * -raySpacing * 2);
        Vector2 _rayOrigin3 = boxCollider2D.bounds.max + (Vector3.right * -raySpacing * 3);


        RaycastHit2D hit0 = Physics2D.Raycast(_rayOrigin0, Vector2.up, rayLength, layerMask);
        RaycastHit2D hit1 = Physics2D.Raycast(_rayOrigin1, Vector2.up, rayLength, layerMask);
        RaycastHit2D hit2 = Physics2D.Raycast(_rayOrigin2, Vector2.up, rayLength, layerMask);
        RaycastHit2D hit3 = Physics2D.Raycast(_rayOrigin3, Vector2.up, rayLength, layerMask);
        if (hit0.collider == null && hit1.collider == null && hit2.collider == null && hit3.collider == null)
        {
            Debug.DrawRay(_rayOrigin0, Vector2.up * rayLength, Color.green);
            Debug.DrawRay(_rayOrigin1, Vector2.up * rayLength, Color.green);
            Debug.DrawRay(_rayOrigin2, Vector2.up * rayLength, Color.green);
            Debug.DrawRay(_rayOrigin3, Vector2.up * rayLength, Color.green);
            isCieled = false;
        }
        else
        {
            Debug.DrawRay(_rayOrigin0, Vector2.down * rayLength, Color.red);
            Debug.DrawRay(_rayOrigin1, Vector2.down * rayLength, Color.red);
            Debug.DrawRay(_rayOrigin2, Vector2.down * rayLength, Color.red);
            Debug.DrawRay(_rayOrigin3, Vector2.down * rayLength, Color.red);
            isCieled = true;
        }

    }
    #endregion

    #region CALCULATING GRAVITY
    [Header("GRAVITY")]
    [SerializeField] private float defaultGravity = 1f;
    [SerializeField][Range(1, 50)] private float gravityUp = 15f;
    [SerializeField][Range(1, 50)] private float gravityDown = 20f;
    [SerializeField] private float fallCap = 30f;
    void CalculateGravity()
    {
        if (rigiBody2D.velocity.y > 0)
        {
            rigiBody2D.gravityScale = gravityUp;
        }
        else if (rigiBody2D.velocity.y < 0)
        {
            rigiBody2D.gravityScale = gravityDown;
        }
        else
        {
            rigiBody2D.gravityScale = defaultGravity;
        }

        if (rigiBody2D.velocity.y < -fallCap)
        {
            rigiBody2D.velocity = new Vector2(rigiBody2D.velocity.x, -fallCap);
        }

    }
    #endregion

    #region JUMPING
    [Header("JUMPING")]

    [SerializeField] private bool _jumping = false;
    [SerializeField] private float _jumpDuration;
    [SerializeField] private float _jumpBuffer = 1f;

    
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private bool startBufferTimer;
    [SerializeField] private bool isJumpbuffered = false;
    [SerializeField] private float timerForJump = 0f;
    [SerializeField] private float timerForJumpBuffer = 0;
    public bool _airbone;
    public float _airTime;
    void CalculateJump()
    {
        if(jumpDown)
        {
            if(!isGrounded)
            {
                startBufferTimer = true;
            }
            if(isGrounded)
            {
                _jumping = true;
                isJumpbuffered = false;
                timerForJump = 0;
                timerForJumpBuffer = 0;
                _airTime = 0;
            }
            
        }
        
        if(isJumpbuffered)
        {
            _jumping = true;
            isJumpbuffered = false;
            timerForJump = 0;
            timerForJumpBuffer = 0;
            _airTime = 0;
        }

        if(_jumping && !isGrounded)
        {
            _airbone = true;
            
        }

        if(_jumping)
        {
            if (timerForJump < _jumpDuration / 2) 
            {
                timerForJump += Time.deltaTime;
                currentVerticalSpeed = curve.Evaluate(timerForJump);
                rigiBody2D.velocity = new Vector2(rigiBody2D.velocity.x, currentVerticalSpeed); 
            }
        }
        
        if(_jumping && rigiBody2D.velocity.y > 0 && jumpUp)
        {
            _jumping = false;
            
            rigiBody2D.velocity = new Vector2(rigiBody2D.velocity.x, 0);
        }

        if (jumpUp && rigiBody2D.velocity.y < 0)
        {
            _jumping = false;
        }

        if (_jumping && isCieled && rigiBody2D.velocity.y>0)
        {
            _jumping = false;
        }

        if (_airbone)
        {
            _airTime += Time.deltaTime;
        }
        if(_airbone && isGrounded)
        {
            _airbone = false;
            //_jumping = false;
            startBufferTimer = false;

        }
        if(isGrounded)
        {
            startBufferTimer = false;
        }
        if(isGrounded && (timerForJumpBuffer < _jumpBuffer&&timerForJumpBuffer > 0))
        {
            isJumpbuffered = true;
        }

        if(startBufferTimer)
        {
            timerForJumpBuffer += Time.deltaTime;
        }

        if(jumpUp && _jumping)
        {
            _jumping = false;
        }
    }

    #endregion
}
