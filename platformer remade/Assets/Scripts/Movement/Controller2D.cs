using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller2D : MonoBehaviour
{
    const float SKIN_WIDTH = 0.015f;

    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private RaycastOrigins raycastOrigins;

    [SerializeField]private int horizontalRayCount = 4, verticalRayCount = 4;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private float horizontalRaySpacing, verticalRaySpacing;
    [SerializeField] public CollisionInfo collisions;

   
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        CalculateRaySpacing();
    }

    private void Update()
    {
        
        

        
    }

    public void Move(Vector2 velocity)
    {
        UpdateRaycastOrigins();
        if (velocity.x !=0)
        {
            HorizontalCollisions(ref velocity);
        }
        if (velocity.y !=0)
        {
            VerticalCollisions(ref velocity);
        }
        transform.Translate(velocity);
    }

    void HorizontalCollisions(ref Vector2 velocity)
    {
        float _directionX = Mathf.Sign(velocity.x);
        float _rayLength = Mathf.Abs(velocity.x) + SKIN_WIDTH;
        for (int i = 0; i < horizontalRayCount; i++)
        {
            Vector2 _rayOrigin = (_directionX == -1) ? raycastOrigins.BottomLeft : raycastOrigins.BottomRight;
            _rayOrigin += Vector2.up * (horizontalRaySpacing * i);
            RaycastHit2D hit = Physics2D.Raycast(_rayOrigin, Vector2.right * _directionX, _rayLength, collisionMask);

            Debug.DrawRay(_rayOrigin, Vector2.right * _directionX * _rayLength, Color.red);

            if (hit.collider != null)
            {
                velocity.x = (hit.distance - SKIN_WIDTH) * _directionX;
                _rayLength = hit.distance;

                collisions.right = (_directionX == 1);
                collisions.left = (_directionX == -1);
            }
            else
            {
                collisions.Reset();
            }
        }
    }

    void VerticalCollisions(ref Vector2 velocity) 
    {
        float _directionY = Mathf.Sign(velocity.y);
        float _rayLength = Mathf.Abs(velocity.y) + SKIN_WIDTH;
        for (int i = 0; i < verticalRayCount; i++)
        {
            Vector2 _rayOrigin = (_directionY == -1) ? raycastOrigins.BottomLeft : raycastOrigins.TopLeft;
            _rayOrigin += Vector2.right * (verticalRaySpacing * i + velocity.x);
            RaycastHit2D hit = Physics2D.Raycast(_rayOrigin, Vector2.up * _directionY, _rayLength, collisionMask);

            Debug.DrawRay(_rayOrigin, Vector2.up * _directionY * _rayLength, Color.red);

            if (hit.collider != null)
            {
                velocity.y = (hit.distance - SKIN_WIDTH) * _directionY;
                _rayLength = hit.distance;

                collisions.above = (_directionY == 1);
                collisions.below = (_directionY == -1);
            }
            else
            {
                collisions.Reset();
            }
        }
    }
    void UpdateRaycastOrigins()
    {
        Bounds _bounds = boxCollider2D.bounds;
        _bounds.Expand(SKIN_WIDTH * -2);

        raycastOrigins.BottomLeft = new Vector2(_bounds.min.x, _bounds.min.y);
        raycastOrigins.BottomRight = new Vector2(_bounds.max.x, _bounds.min.y);
        raycastOrigins.TopLeft = new Vector2(_bounds.min.x, _bounds.max.y);
        raycastOrigins.TopRight = new Vector2(_bounds.max.x, _bounds.max.y);
    }

    void CalculateRaySpacing()
    {
        Bounds _bounds = boxCollider2D.bounds;
        _bounds.Expand(SKIN_WIDTH * -2);

        horizontalRayCount = Mathf.Clamp(horizontalRayCount,2,int.MaxValue);
        verticalRayCount = Mathf.Clamp(verticalRayCount,2,int.MaxValue);

        horizontalRaySpacing = _bounds.size.y/(horizontalRayCount-1);
        verticalRaySpacing = _bounds.size.x / (verticalRayCount - 1);
    }
    struct RaycastOrigins
    {
        public Vector2 TopLeft; 
        public Vector2 TopRight;
        public Vector2 BottomLeft;
        public Vector2 BottomRight;    
    }

    public struct CollisionInfo
    {
        public bool above, below;
        public bool left, right;

        public void Reset()
        {
            above = false;
            below = false;
            left = false;
            right = false;
        }
    }
}
