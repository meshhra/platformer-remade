using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementByTranslation : MonoBehaviour
{
    
    private void Start()
    {
        
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
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _jumpHeight = 20;

    private void SetHorizontalvelocity()
    {
        if (xInput != 0)
        {
            _horizontalSpeed = Mathf.MoveTowards(_horizontalSpeed, _speed * xInput, _acceleration * Time.deltaTime);
        }
        else
        {
            _horizontalSpeed = Mathf.MoveTowards(_horizontalSpeed, 0f, _deacceleration * Time.deltaTime);
        }
        
    }

    private void SetverticalSpeed()
    {
        if (jumpDown)
        {
            
        }
    }
    
    public struct FrameInput
    {
        public int X;
        public bool JumpUp;
        public bool JumpDown;
    }

    
}
