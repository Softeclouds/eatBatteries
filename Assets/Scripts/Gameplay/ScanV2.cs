using UnityEngine;

public class OutlineManager : MonoBehaviour
{
    public GameObject[] objectsWithOutline;  // Objects to apply the outline effect
    private bool outlineActive = false;      // Track if the outline is active or not
    private float outlineTimer = 0f;         // Timer to track how long the outline is active
    public float outlineDuration = 3f;       // How long the outline stays active (in seconds)

    // Start is called before the first frame update
    void Start()
    {
        // Make sure all objects start without the outline effect
        SetOutlineVisibility(false);  // Ensure outline starts off by setting to 0 (no outline)
    }

    // Update is called once per frame
    void Update()
    {
        // Toggle outline on press of the "R" key
        if (Input.GetKeyDown(KeyCode.R) && !outlineActive)
        {
            outlineActive = true;
            outlineTimer = outlineDuration;  // Reset the timer to the duration
            SetOutlineVisibility(true);  // Activate the outline
        }

        // If the outline is active, count down the timer
        if (outlineActive)
        {
            outlineTimer -= Time.deltaTime;

            // If the timer has elapsed, deactivate the outline
            if (outlineTimer <= 0f)
            {
                outlineActive = false;
                SetOutlineVisibility(false);  // Deactivate the outline
            }
        }
    }

    // Set outline visibility on the specified objects
    void SetOutlineVisibility(bool isActive)
    {
        foreach (var obj in objectsWithOutline)
        {
            Renderer renderer = obj.GetComponent<Renderer>();
            if (renderer != null)
            {
                Material[] materials = renderer.materials;

                // Check each material and enable or disable the outline material
                foreach (var material in materials)
                {
                    // correct sahder
                    if (material.shader.name == "Shader Graphs/Outliner")
                    {
                        // Adjust the outline width (enable or disable)
                        float outlineWidth = isActive ? 1.11f : 0f; // Set outline width to 0 if not active, or 1.11 if active
                        material.SetFloat("_Scale", outlineWidth);  // Set the outline scale
                    }
                }
            }
        }
    }
}
