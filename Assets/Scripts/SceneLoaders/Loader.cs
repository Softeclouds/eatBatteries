using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1f;
    private bool timelineFinished = false;

    
    void Update()
    {
        
        if (timelineFinished)
        {
            LoadNextLevel();
        }
    }

    public void timelinebool()
    {
        timelineFinished = true;
    }
    public void LoadNextLevel()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        Debug.Log("Loading scene: " + levelIndex);

        // Play transition animation
        transition.SetTrigger("Start");

        // Wait for the animation to finish before loading the next scene
        yield return new WaitForSeconds(transitionTime);

        // Load the next scene
        SceneManager.LoadScene(levelIndex);

    }
}
