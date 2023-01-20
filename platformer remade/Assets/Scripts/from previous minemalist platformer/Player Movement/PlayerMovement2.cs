using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody2D>(); 
    }

    public Vector2 _vel ;
    void Update()
    {
        _vel = playerRb.velocity;
        GatherInput();
        
        CalculateWalk();
        //CalculateGravity();
        
    }

    void FixedUpdate()
    {
        MovePLayer();
        CalculateJump();
    }

    [SerializeField]private float _xInput;
    [SerializeField]private bool _jumpUp;
    [SerializeField]private bool _jumpDown;

    void GatherInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");
        _jumpDown = Input.GetKeyDown(KeyCode.Space);
        _jumpUp = Input.GetKeyUp(KeyCode.Space);
    }



    [SerializeField]private float _curruntHorizontaVelocity;
    [SerializeField]private float _targetHorizontalVelocity = 13f;
    [SerializeField]private float _acceleration = 90f;
    [SerializeField]private float _retardation = 90f;
    
    void CalculateWalk()
    {
        if (_xInput !=0 )
        {
            _curruntHorizontaVelocity += _xInput * _acceleration * Time.deltaTime; 
            _curruntHorizontaVelocity = Mathf.Clamp(_curruntHorizontaVelocity,-_targetHorizontalVelocity,_targetHorizontalVelocity);
        }
        else
        {
            _curruntHorizontaVelocity = Mathf.MoveTowards(_curruntHorizontaVelocity,0,_retardation * Time.deltaTime);
        }
    }

    [SerializeField]private float _currentVertivalVelocity;
    [SerializeField]private float _fallSpeed = 15f;
    void CalculateGravity()
    {
        _currentVertivalVelocity = -_fallSpeed;
    }


    [SerializeField]private float rayLength = 0.6f;
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


    [SerializeField]private float _jumpHeight;
   
    void CalculateJump()
    {
        if (_jumpDown )
        {
            _currentVertivalVelocity = _jumpHeight;
            playerRb.velocity = new Vector2(playerRb.velocity.x,_currentVertivalVelocity) * Time.deltaTime;    
        }
   
        
    }


    void MovePLayer()
    {
        playerRb.velocity = new Vector2(_curruntHorizontaVelocity,playerRb.velocity.y) * Time.deltaTime;
    }
    public Rigidbody2D playerRb;
    
}
