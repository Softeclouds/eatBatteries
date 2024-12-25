using UnityEngine;
using TMPro;


public class PlayerInteraction : MonoBehaviour
{
    public float interactionDistance = 5f;  // Max distance to interact with objects
    public LayerMask interactableLayer;     // Layer to check for interactable objects
    public KeyCode interactKey = KeyCode.E; // Key to trigger the interaction
    private Transform playerTransform;      // Player's position

    public string interactionType;

    public GameObject uiPanel; // The UI panel to show/hide
    public TextMeshProUGUI uiText; 
    private void Start()
    {
        uiPanel.SetActive(false);
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
                ObjectInteractions objectInteractions = interactableObject.GetComponent<ObjectInteractions>();

                if (objectInteractions != null)
                {
                    // Depending on the InteractionType, perform the appropriate action
                    switch (objectInteractions.InteractionType)
                    {
                        case "Collection":
                            Collect(interactableObject.gameObject);
                            break;
                        case "Informative":
                            Info(interactableObject.gameObject);
                            //ShowUI();
                            break;
                        default: // Incase type is not set
                            Debug.Log("Unknown interaction type: " + objectInteractions.InteractionType);
                            break;
                    }
                }
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

    private void Collect(GameObject interactableObject)
    {
        // Checking to see if its getting the right objs
        Debug.Log("Collected: " + interactableObject.name);

        // delete object thats interacted with
        Destroy(interactableObject); 
    }

    private void Info(GameObject interactableObject)
    {
        // Checking to see if its getting the right objs
        Debug.Log("Checking info of: " + interactableObject.name); 
        // bring up UI for information
        //ShowUI();
        // close UI
        if (Input.anyKeyDown || Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            CloseUI();
        }
    }

   /* public void ShowUI(string message = "Press any key or mouse button to close.")
    {
        uiPanel.SetActive(true);   // Show the UI panel
        if (uiText != null)
        {
            uiText.text = message; 
        }
    } */

    private void CloseUI()
    {
        uiPanel.SetActive(false);  // Hide the UI panel
    }
}