using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Variables 
    public float speed = 5f; // player speed
    public float groundDist; // distance to ground
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
        
    }
    // user defined functions
    void Movement()
    {

    }

    void GroundCheck()
    {

    }
    
    // end of user defined functions

}
