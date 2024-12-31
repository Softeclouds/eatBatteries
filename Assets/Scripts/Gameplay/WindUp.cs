using System.Data;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using Unity.Collections;

public class WindUp : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // variables
    public Image windUpImage; // UI image for winding up
    public RectTransform windUpArea; // the area in which you can wind up
    public float windUpSpeed = 10f; // How fast the windup occurs
    public float maxWindUp = 100f; // max value
    public float currentWindUp = 0f; // current value
    private bool isDragging = false;
    private Vector2 lastMousePosition;


    public Volume volume;
    private Vignette vignette;
     public float initialVignetteIntensity = 0.8f; // Starting intensity of the vignette


     public float decayRate = 5f; // how quickly it depletes
     public float depletionTimer = 0f; // timer to track when to deplete
    // end of variables
    void Start()
    {
        // Try to get the Vignette component from the volume profile
        if (volume.profile.TryGet<Vignette>(out vignette))
        {
            // Ensure Vignette is enabled in the volume
            vignette.active = true;
            // start vignette at initial value
            vignette.intensity.value = initialVignetteIntensity;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // start dragging
        {
            isDragging = true;
            lastMousePosition = Input.mousePosition;

            // Reset the depletion timer when the player starts dragging
            depletionTimer = 0f;
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
                //Debug.Log("Current Wind-Up: " + currentWindUp);

                AdjustVignetteIntensity();

                // update the last mouse position
                lastMousePosition = mousePosition;
            }
        }
        else
        {
            // If not dragging, deplete the wind-up over time
            DepleteWindUpOverTime();
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

    // Adjust the vignette intensity based on the current wind-up value
    private void AdjustVignetteIntensity()
    {
        if (vignette != null)
        {
            // Map the current wind-up (0 to maxWindUp) to vignette intensity (0 to 1)
            float intensity = 1f - (currentWindUp / maxWindUp); // Higher wind-up, less dark
            vignette.intensity.value = intensity;  // Set the vignette intensity
        }
    }

    private void DepleteWindUpOverTime()
    {
        // Increase the depletion timer as time passes
        depletionTimer += Time.deltaTime;

        // If the depletion timer has reached the decay rate, deplete the wind-up
        if (depletionTimer >= decayRate)
        {
            currentWindUp -= 1f * Time.deltaTime; // Slow depletion
            currentWindUp = Mathf.Clamp(currentWindUp, 0f, maxWindUp);

            // Update the fill amount of the image to show the wind-up progress
            windUpImage.fillAmount = currentWindUp / maxWindUp;

            // Adjust the vignette intensity based on the current wind-up value
            AdjustVignetteIntensity();
        }
    }
}
