using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Variables 
    public float speed = 5f; // player speed
    public float groundDist = 0.5f; // distance to ground
    public LayerMask terrainLayer;
    public Rigidbody rb;
  
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
        if (Physics.Raycast(castPos, -Vector3.up, out hit, groundDist + 0.1f, terrainLayer))
        {
            Vector3 movePos = transform.position;
            movePos.y = hit.point.y + groundDist;

            // If the player is grounded, apply the position using MovePosition
            rb.MovePosition(movePos);
        }
    }

    // end of user defined functions

}
