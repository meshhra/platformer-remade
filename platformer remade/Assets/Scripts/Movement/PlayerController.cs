using UnityEngine;
using UnityEngine.Serialization;

namespace Movement
{
    public class PlayerController : MonoBehaviour
    {
        [FormerlySerializedAs("rigiBody2D")]
        [SerializeField] private Rigidbody2D rigidBody2D;

        [SerializeField] private BoxCollider2D boxCollider2D;

        private void Start()
        {
            rigidBody2D = GetComponent<Rigidbody2D>();
            boxCollider2D = GetComponent<BoxCollider2D>();
            Physics2D.queriesStartInColliders = false;
        }

        [SerializeField] private Vector2 velocity;
        private void Update()
        {
            velocity = rigidBody2D.velocity;
            GetInput();
            SetHorizontalSpeed();
        
            CheckGrounded();
            CheckCeiling();
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

        private void SetHorizontalSpeed()
        {
            if (inputX != 0)
            {
                currentHorizontalSpeed += Time.deltaTime * movementSpeed * acceleration * inputX;
                currentHorizontalSpeed = Mathf.Clamp(currentHorizontalSpeed, -movementSpeed, movementSpeed);
            }
            else
            {
                currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, 0, deacceleration * Time.deltaTime);
            }
            rigidBody2D.velocity = new Vector2(currentHorizontalSpeed, rigidBody2D.velocity.y);
        }
        #endregion

        #region GROUND CHECK
        [Header("GROUND CHECK")]
        [SerializeField] private bool isGrounded = false;
        [SerializeField] private LayerMask layerMask;
        [SerializeField] private float rayLength = 1f;
        [SerializeField] private float raySpacing;
        private const int RAY_COUNT = 4;


        public void CheckGrounded()
        {
            raySpacing = boxCollider2D.bounds.size.x / (RAY_COUNT - 1);
            var _positionNow = transform.position;
            Vector2 _rayOrigin0 = (_positionNow - new Vector3(0.4f, 0.4f, 0));
            Vector2 _rayOrigin1 = (_positionNow - new Vector3(0.4f, 0.4f, 0)) + (Vector3.right * (raySpacing * 1));
            Vector2 _rayOrigin2 = (_positionNow - new Vector3(0.4f, 0.4f, 0)) + (Vector3.right * (raySpacing * 2));
            Vector2 _rayOrigin3 = (_positionNow - new Vector3(0.4f, 0.4f, 0)) + (Vector3.right * (raySpacing * 3));


            RaycastHit2D _hit0 = Physics2D.Raycast(_rayOrigin0, Vector2.down, rayLength);
            RaycastHit2D _hit1 = Physics2D.Raycast(_rayOrigin1, Vector2.down, rayLength);
            RaycastHit2D _hit2 = Physics2D.Raycast(_rayOrigin2, Vector2.down, rayLength);
            RaycastHit2D _hit3 = Physics2D.Raycast(_rayOrigin3, Vector2.down, rayLength);
            if (_hit0.collider == null && _hit1.collider == null && _hit2.collider == null && _hit3.collider == null)
            {
            
                Debug.DrawRay(_rayOrigin1, Vector2.down * rayLength, Color.green);
                Debug.DrawRay(_rayOrigin2, Vector2.down * rayLength, Color.green);
                Debug.DrawRay(_rayOrigin3, Vector2.down * rayLength, Color.green);
                isGrounded = false;
            }
            else
            {
                //Debug.Log($"{hit0.collider.name}");
                Debug.DrawRay(_rayOrigin0, Vector2.down * rayLength, Color.red);
                Debug.DrawRay(_rayOrigin1, Vector2.down * rayLength, Color.red);
                Debug.DrawRay(_rayOrigin2, Vector2.down * rayLength, Color.red);
                Debug.DrawRay(_rayOrigin3, Vector2.down * rayLength, Color.red);
                isGrounded = true;
            }

        }
        #endregion

        #region CEILING CHECK
        [FormerlySerializedAs("isCieled")]
        [Header("CEILING CHECK")]
        [SerializeField] private bool isCeiled = false;


        private void CheckCeiling()
        {
            var _positionNow = transform.position;
            raySpacing = boxCollider2D.bounds.size.x / (RAY_COUNT - 1);
            Vector2 _rayOrigin0 = (_positionNow + new Vector3(0.4f, 0.4f, 0));
            Vector2 _rayOrigin1 = (_positionNow + new Vector3(0.4f, 0.4f, 0)) + (Vector3.right * (-raySpacing * 1));
            Vector2 _rayOrigin2 = (_positionNow + new Vector3(0.4f, 0.4f, 0)) + (Vector3.right * (-raySpacing * 2));
            Vector2 _rayOrigin3 = (_positionNow + new Vector3(0.4f, 0.4f, 0)) + (Vector3.right * (-raySpacing * 3));


            RaycastHit2D _hit0 = Physics2D.Raycast(_rayOrigin0, Vector2.up, rayLength, layerMask);
            RaycastHit2D _hit1 = Physics2D.Raycast(_rayOrigin1, Vector2.up, rayLength, layerMask);
            RaycastHit2D _hit2 = Physics2D.Raycast(_rayOrigin2, Vector2.up, rayLength, layerMask);
            RaycastHit2D _hit3 = Physics2D.Raycast(_rayOrigin3, Vector2.up, rayLength, layerMask);
            if (_hit0.collider == null && _hit1.collider == null && _hit2.collider == null && _hit3.collider == null)
            {
                Debug.DrawRay(_rayOrigin0, Vector2.up * rayLength, Color.green);
                Debug.DrawRay(_rayOrigin1, Vector2.up * rayLength, Color.green);
                Debug.DrawRay(_rayOrigin2, Vector2.up * rayLength, Color.green);
                Debug.DrawRay(_rayOrigin3, Vector2.up * rayLength, Color.green);
                isCeiled = false;
            }
            else
            {
                Debug.DrawRay(_rayOrigin0, Vector2.down * rayLength, Color.red);
                Debug.DrawRay(_rayOrigin1, Vector2.down * rayLength, Color.red);
                Debug.DrawRay(_rayOrigin2, Vector2.down * rayLength, Color.red);
                Debug.DrawRay(_rayOrigin3, Vector2.down * rayLength, Color.red);
                isCeiled = true;
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
            if (rigidBody2D.velocity.y > 0)
            {
                rigidBody2D.gravityScale = gravityUp;
            }
            else if (rigidBody2D.velocity.y < 0)
            {
                rigidBody2D.gravityScale = gravityDown;
            }
            else
            {
                rigidBody2D.gravityScale = defaultGravity;
            }

            if (rigidBody2D.velocity.y < -fallCap)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, -fallCap);
            }

        }
        #endregion

        #region JUMPING

        [Header("Jumping")] 
        [SerializeField] private AnimationCurve jumpCurve;


        private void CalculateJump()
        {
            if (isGrounded)
            {
                if (jumpDown)
                {
                    rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 40);
                }
            }

            if (jumpUp)
            {
                rigidBody2D.velocity = new Vector2(rigidBody2D.velocity.x, 0);
            }
            
        }

        #endregion
    }
}
