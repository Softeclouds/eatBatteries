using System.Collections;
using UnityEngine;

public class Scanning : MonoBehaviour
{
   
    public float scanDuration = 3f;
    public GameObject[] scannables;         // Objects to enable/disable
    public GameObject[] scanObject;        // Objects whose materials will be changed
    public Material newMaterial;           // The glowing material
    private Material[] originalMaterials;  // Array to store original materials for each object

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the array to store the original materials for each scanObject
        originalMaterials = new Material[scanObject.Length];

        // Store the original material for each object in scanObject
        for (int i = 0; i < scanObject.Length; i++)
        {
            originalMaterials[i] = scanObject[i].GetComponent<Renderer>().material;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the R key is pressed
        if (Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed!");
            StartCoroutine(EnableDisable());
        }
    }

    // Coroutine to enable objects, change material, and then revert it after a time
    private IEnumerator EnableDisable()
    {


        foreach (GameObject obj in scannables)
        {
            obj.SetActive(true);
            Debug.Log($"Enabled {obj.name}");
        }

        // Change the material of each object in scanObject to the glowing material before disabling
        foreach (GameObject obj in scanObject)
        {
            obj.GetComponent<Renderer>().material = newMaterial;
            Debug.Log($"Changed material for {obj.name} to glowing material");
        }

        // Wait for the scan duration (while the glowing material is applied)
        yield return new WaitForSeconds(scanDuration);

        // Disable all objects with the specified tag
        foreach (GameObject obj in scannables)
        {
            obj.SetActive(false);
            Debug.Log($"Disabled {obj.name}");
        }

        // Revert the material for each object back to the original material
        for (int i = 0; i < scanObject.Length; i++)
        {
            scanObject[i].GetComponent<Renderer>().material = originalMaterials[i];
            Debug.Log($"Reverted material for {scanObject[i].name} to original material");
        }
    }
}
