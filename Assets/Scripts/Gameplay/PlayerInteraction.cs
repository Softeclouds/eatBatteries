using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 5f;  // Max distance to interact with objects
    public LayerMask interactableLayer;     // Layer to check for interactable objects
    public KeyCode interactKey = KeyCode.E; // Key to trigger the interaction
    private Transform playerTransform;      // Player's position

    private void Start()
    {
        playerTransform = transform;  // The player's transform (position and rotation)
    }

    private void Update()
    {
        HandleInteraction();
    }

    private void HandleInteraction()
    {
        // Find all objects on the "Interactable" layer within the interaction range
        Collider[] interactableObjects = Physics.OverlapSphere(playerTransform.position, interactionDistance, interactableLayer);

        foreach (var interactableObject in interactableObjects)
        {
            // Check if the player presses the interact button
            if (Input.GetKeyDown(interactKey))
            {
                InteractWithObject(interactableObject.gameObject); 
            }
        }
    }

    private void InteractWithObject(GameObject interactableObject)
    {
        // Checking to see if its getting the right objs
        Debug.Log("Interacted with: " + interactableObject.name);

        // delete object thats interacted with
        Destroy(interactableObject); 
    }
}