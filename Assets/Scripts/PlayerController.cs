using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Variables 
                //public variables appear in the inspector, private dont//
    public float speed = 5f; // player speed
    public float groundDist = 0.5f; // distance to ground
    public LayerMask terrainLayer; // only checking a specific layer
    public Rigidbody rb;
    public SpriteRenderer sr;
  
    // end of variables





    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>(); // initializing the rigidbody
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous; // Ensure continuous collision detection 
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        GroundCheck();
        SpriteFlip();
    }
    // user defined functions
    void Movement()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector3 movement = (transform.right * x + transform.forward * y).normalized;
        Vector3 targetVelocity = movement * speed;

        // Apply velocity to the Rigidbody, keeping the y-axis unchanged for gravity
        Vector3 velocity = rb.linearVelocity;
        velocity.x = targetVelocity.x;
        velocity.z = targetVelocity.z;

        // Apply the new velocity to the Rigidbody
        rb.linearVelocity = velocity;
    }

    void GroundCheck()
    {
        RaycastHit hit;
        Vector3 castPos = transform.position + Vector3.up * 0.5f; // Start raycast slightly above the player
        if (Physics.Raycast(castPos, -Vector3.up, out hit, groundDist + 0.1f, terrainLayer)) // checking if the ray hits the ground
        {
            Vector3 movePos = transform.position;
            movePos.y = hit.point.y + groundDist;

            // If the player is grounded, apply the position using MovePosition
            rb.MovePosition(movePos);
        }
    }

    void SpriteFlip() // fliping the sprite depending on the direction the player is moving
    {
        float x = Input.GetAxis("Horizontal");
        if (x != 0)
        {
            sr.flipX = x < 0;
        }
    }



    // end of user defined functions

}
