using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>();
        
    }

    // Update is called once per frame
    public Vector2 velocity;
    public float fallCap;
    void Update()
    {
        velocity = playerRb.velocity;
        if (_isDashing) return;
        GatherInput();
        GroundCheck();
        CalculateWalk();
        
        //CalculateJumpApex();
        CalculateGravity();
        CalculateJump();
        CalculateDash();
         

    }

    
    

    private float horizontalInput;
    private bool jumpUp;
    private bool jumpDown;
    private bool jumpStay;
    private bool dashInput;

    void GatherInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        jumpUp = Input.GetKeyUp(KeyCode.Space);
        jumpDown = Input.GetKeyDown(KeyCode.Space);
        jumpStay = Input.GetKey(KeyCode.Space);
        dashInput = Input.GetKeyDown(KeyCode.LeftShift);
    }

    [Header("GROUND CHECK")]
    private float rayLength = 0.6f;
    public LayerMask groundMask;
    public bool grounded;
    void GroundCheck()
    {
        RaycastHit2D rayHit = Physics2D.Raycast(transform.position,Vector2.down,rayLength,groundMask);
        if (rayHit.collider != null)
        {
            Debug.DrawLine(transform.position,transform.position - new Vector3 (0,rayLength,0),Color.red);
            grounded=true;
            //Debug.Log("grounded");
        }
        else
        {
            Debug.DrawLine(transform.position,transform.position - new Vector3 (0,rayLength,0), Color.green);
            grounded = false;
            //Debug.Log("not grounded");
        }
    }

    [Header ("MOVE")]
    [SerializeField]private float targetHorizotalSpeed = 13f;
    private float currentHorizontalSpeed;
    [SerializeField]private float acceleration;
    [SerializeField]private float retardation;

    

    //[SerializeField]private int _lookDirection = 1;// 1 if right -1 if left by default right

    void CalculateWalk()
    {

        


        if (horizontalInput!=0)
        {
            currentHorizontalSpeed += horizontalInput * Time.deltaTime * acceleration;
            currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed,-targetHorizotalSpeed,targetHorizotalSpeed);
        }
        else
        {
            currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed,0f,retardation * Time.deltaTime);
        }
        playerRb.velocity = new Vector2(currentHorizontalSpeed,playerRb.velocity.y);
    }

    




    

    

    [Header ("GRAVITY")]
    [SerializeField]private float _gravityUpModifier = 12f;
    [SerializeField]private float _gravityDownModifier = 15f;
    [SerializeField]private float _defaultGravityModifier = 12f;

    private float _fallSpeed;
    
    void CalculateGravity()
    {
        
        
        if (velocity.y>0 && !_isDashing)
        {
            _fallSpeed = _gravityUpModifier;
        }
        else if (velocity.y < 0 && !_isDashing)
        {
            _fallSpeed = _gravityDownModifier;
        }
        else if (velocity.y == 0 && !_isDashing)
        {
            _fallSpeed = _defaultGravityModifier;
        }

        else if (_isDashing)
        {
            _fallSpeed = 0f;
        }
        
        playerRb.gravityScale = _fallSpeed;

        
        //clamp fall velocity
        float y = playerRb.velocity.y;
        y = Mathf.Clamp(y,-fallCap,jumpHeight);
        playerRb.velocity = new Vector2(playerRb.velocity.x,y);
        
        
    }


    [Header("JUMP")]
    [SerializeField]private float jumpHeight;

    [SerializeField]private bool endedJumpEarly;
    [SerializeField]private int jumpPhase = 0;
    [SerializeField]private int maxAirJumps=1;
    public float temp;
    private float currentVerticalSpeed;
    

    
    void CalculateJump()
    {
        
        if (jumpDown && (grounded || jumpPhase < maxAirJumps))
        {
            endedJumpEarly = false;
            currentVerticalSpeed=jumpHeight;
            playerRb.velocity = new Vector2(playerRb.velocity.x,currentVerticalSpeed);
            jumpPhase++;
            
        }

        //use the next if statement to get variable jump height.

        if (!grounded && jumpUp && !endedJumpEarly && velocity.y > 0)
        {
            
            currentVerticalSpeed = currentVerticalSpeed/temp;
            playerRb.velocity = new Vector2(playerRb.velocity.x,currentVerticalSpeed);
            endedJumpEarly = true;
        }
        // works by setting the player vertical velocity to 0 (or to a desired velocity) when the jump key is released
        
        if (grounded && jumpPhase !=0)
        {
            jumpPhase = 0;
        }



        
        
    }

    

    [Header ("DASH")]
    [SerializeField]private float _dashStartTime = 0.1f;
    [SerializeField]private float _dashTime;
    [SerializeField]private float _dashSpeed = 50f;
    [SerializeField]private float _dashCooldon = 0.05f;

    [SerializeField]private bool _canDash = true;
    [SerializeField]private bool _isDashing;

    [SerializeField]private int _lookDirection = 1;
    [SerializeField]private int _dashPhase = 0;
    [SerializeField]private int _maxAirDash = 2;
/*     void CalculateDash()
    {

        if (Input.GetKey(KeyCode.A))
        {
            //transform.localScale = new Vector3(-1,1,1);
            _lookDirection = -1;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            //transform.localScale = new Vector3(1,1,1);
            _lookDirection = 1;
        }


        if (!_isDashing && dashInput)
        {
            _isDashing = true;
            _dashTime = _dashStartTime;

        }
        if (_dashTime > 0)
        {
            
            if (_lookDirection == 1)
            {
                playerRb.velocity = new Vector2 (_dashSpeed,0f);
            }
            if (_lookDirection == -1)
            {
                playerRb.velocity = new Vector2 (-_dashSpeed,0f);
            }
            retardation = 999f;
            _dashTime -= Time.deltaTime;
        }
        else if (_dashTime == 0 || _dashTime < 0)
        {
            _isDashing = false;
        }
        
    } */

    void CalculateDash()
    {
        if (grounded) _dashPhase = 0;
        if (Input.GetKey(KeyCode.A)||Input.GetKeyDown(KeyCode.LeftArrow))
        {
            //transform.localScale = new Vector3(-1,1,1);
            _lookDirection = -1;
        }
        else if (Input.GetKey(KeyCode.D)||Input.GetKeyDown(KeyCode.RightArrow))
        {
            //transform.localScale = new Vector3(1,1,1);
            _lookDirection = 1;
        }
        if (dashInput && _canDash && !_isDashing)
        {   
            if (!grounded)
            {
                _dashPhase ++;
                
            }


            if (_dashPhase <=_maxAirDash)
            {
                StartCoroutine("Dash");
            }
            
        }
    }
    private IEnumerator Dash()
    {
        _canDash = false;
        _isDashing = true;
        playerRb.gravityScale = 0f;
        if (_lookDirection == 1)
        {
            playerRb.velocity = new Vector2(_dashSpeed,0f);
        }
        if (_lookDirection == -1)
        {
            playerRb.velocity = new Vector2(-_dashSpeed,0f);
        }
        yield return new WaitForSeconds(_dashStartTime);
        _isDashing = false;
        playerRb.gravityScale = _gravityDownModifier;
        yield return new WaitForSeconds(_dashCooldon);
        _canDash = true;
    }

    public Rigidbody2D playerRb;
    
}
