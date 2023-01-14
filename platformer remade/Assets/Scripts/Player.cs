using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Player : MonoBehaviour
{
    [SerializeField]private float gravity = -20f;
    [SerializeField] private Vector2 velocity;

    [SerializeField] private Controller2D controller2D;
    [SerializeField] private Vector2 input;
    [SerializeField] private float moveSpeed = 15f;
    [SerializeField] private float jumpVelocity = 40f;

    public bool below;

    // Start is called before the first frame update
    void Start()
    {
        controller2D= GetComponent<Controller2D>();
    }

    // Update is called once per frame
    void Update()
    {
        below = controller2D.collisions.below;
        if (controller2D.collisions.below || controller2D.collisions.above)
        {
            velocity.y = 0 ;
        }

        if (Input.GetKeyDown(KeyCode.Space) && controller2D.collisions.below)
        {
            velocity.y = jumpVelocity;
        }

        input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        velocity.x = input.x * moveSpeed;
        velocity.y = velocity.y + gravity * Time.deltaTime;
        controller2D.Move(velocity * Time.deltaTime);
    }
}
