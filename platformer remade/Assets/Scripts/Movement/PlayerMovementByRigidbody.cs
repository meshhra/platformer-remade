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
        SetHorizontalvelocity();
        SetverticalSpeed();
    }

    private float xInput;

    private bool jumpDown;
    private bool jumpUp;
    private void GatherInput()
    {
        xInput = Input.GetAxisRaw("Horizontal");
        jumpDown = Input.GetKeyDown(KeyCode.Space);
        jumpUp = Input.GetKeyUp(KeyCode.Space);
    }

    [SerializeField] private float _speed = 12;
    [SerializeField] private float _acceleration = 120;
    [SerializeField] private float _deacceleration = 120;
    [SerializeField]private float _horizontalSpeed;
    [SerializeField] private float _jumpHeight = 20;

    private void SetHorizontalvelocity()
    {
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
        if (jumpDown)
        {
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x,_jumpHeight);
        }
    }
}
