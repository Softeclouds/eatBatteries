using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Scanning : MonoBehaviour
{
    public string targetTag;
    public float scanDuration = 3f;
    public GameObject[] scanables;
    

  
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            Debug.Log("R key pressed!");
            StartCoroutine(EnableDisable());
        }
    }

    private IEnumerator EnableDisable()
    {
         
         Debug.Log($"Found {scanables.Length} objects with the tag '{targetTag}'.");

        foreach (GameObject obj in scanables)
        {
            obj.SetActive(true);
            Debug.Log($"Enabled {obj.name}");
        }

        yield return new WaitForSeconds(scanDuration);

        foreach (GameObject obj in scanables)
        {
            obj.SetActive(false);
            Debug.Log($"Disabled {obj.name}");
        }
    }

    
}
