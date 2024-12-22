using System.Data;
using UnityEngine;
using UnityEngine.UI;

public class WindUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // variables
    public Image windUpImage; // UI image for winding up
    public RectTransform windUpArea; // the area in which you can wind up
    public float windUpSpeed = 10f; // How fast the windup occurs
    public float maxWindUp = 100f; // max value
    private float currentWindUp = 0f; // current value
    private bool isDragging = false;
    private Vector2 lastMousePosition;
    // end of variables
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // start dragging
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0)) // stop dragging
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 mousePosition = Input.mousePosition; // vector2 for 2D space (Don't need a Z cord)

            // check to see if mouse is in the wind up area
            if (IsMouseInsideArea(mousePosition))
            {
                // calculate the direction of the mouse based on the center of the wind up area
                Vector2 center = windUpArea.position;
                Vector2 direction = mousePosition - center;
                // calculate the angle between the current mouse pos and the previous one
                float angleDifference = Vector2.SignedAngle(lastMousePosition - center, mousePosition - center);
                // check if the angle is positive - clockwise rotation
                if (angleDifference < 0)
                // increase the windup based on the windupspeed and delta time
                {
                    currentWindUp += windUpSpeed * Time.deltaTime;
                }
                // restrict the max wind up value
                currentWindUp = Mathf.Clamp(currentWindUp, 0f, maxWindUp);
                // Show values in the log
                Debug.Log("Current Wind-Up: " + currentWindUp);
                Debug.Log("Fill Amount: " + windUpImage.fillAmount);
                // update the last mouse position
                lastMousePosition = mousePosition;
            }
        }
    }
       private bool IsMouseInsideArea(Vector2 mousePosition)
    {
        // Get the RectTransform of the wind-up area
        RectTransform rectTransform = windUpArea;

        // Convert the mouse position to the wind-up area local position
        Vector2 localMousePosition = rectTransform.InverseTransformPoint(mousePosition);

        // Check if the mouse is inside the wind-up area's bounds
        return rectTransform.rect.Contains(localMousePosition);
    }
}
