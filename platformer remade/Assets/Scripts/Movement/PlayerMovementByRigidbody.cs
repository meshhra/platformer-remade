using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class PlayerMovementByRigidbody : MonoBehaviour
{
    [SerializeField]private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D= GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        GatherInput();
    }
    private void FixedUpdate()
    {
        SetGravity();
        SetHorizontalvelocity();
        SetverticalSpeed();
    }

    [Header("INPUT")]
    [SerializeField] private bool wantsTOJump = false;
    private float xInput;
    private bool jumpDown;
    private bool jumpUp;
    

    private void GatherInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        jumpDown = Input.GetKeyDown(KeyCode.Space);
        jumpUp = Input.GetKeyUp(KeyCode.Space);
        if (jumpDown)
        {
            wantsTOJump = true;
        }
    }

    [Header("GRAVITY")]
    [SerializeField][Range(1,50)] private float gravityUp = 10f;
    [SerializeField][Range(1, 50)] private float gravityDown = 15f;

    private void SetGravity()
    {
        Vector2 _velocity = _rigidbody2D.velocity;
        if (_velocity.y > 0)
        {
            _rigidbody2D.gravityScale = gravityUp;
        }
        else if (_velocity.y < 0)
        {
            _rigidbody2D.gravityScale = gravityDown;
        }
        else
        {
            _rigidbody2D.gravityScale = 1;
        }
    }

    [Header("MOVEMENT")]
    [SerializeField] private float _speed = 12;
    [SerializeField] private float _acceleration = 120;
    [SerializeField] private float _deacceleration = 120;
    [SerializeField]private float _horizontalSpeed;
    [SerializeField] private float _jumpHeight = 20;

    private void SetHorizontalvelocity()
    {
        Vector2 _velocity = _rigidbody2D.velocity;
        if (xInput!=0)
        {
            _horizontalSpeed = Mathf.MoveTowards(_horizontalSpeed, _speed * xInput, _acceleration * Time.deltaTime);
        }
        else
        {
            _horizontalSpeed = Mathf.MoveTowards(_horizontalSpeed, 0f, _deacceleration * Time.deltaTime);
        }
        _rigidbody2D.velocity = new Vector2(_horizontalSpeed, _rigidbody2D.velocity.y);

    }

    private void SetverticalSpeed()
    {
        Vector2 _velocity = _rigidbody2D.velocity;
        if (wantsTOJump)
        {
            wantsTOJump = false;
            _rigidbody2D.velocity = new Vector2(_velocity.x,_jumpHeight);
        }
    }
}
