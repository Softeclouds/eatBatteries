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
            // calculate the direction of the mouse based on the center of the wind up area
            // calculate the angle between the current mouse pos and the previous one
            // check if the angle is positive - clockwise rotation
            // increase the windup based on the windupspeed and delta time
            // restrict the max wind up value
            // Show values in the log
            // update the last mouse position
        }
    }
}
