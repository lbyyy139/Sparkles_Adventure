

using UnityEngine;
using UnityEngine.SceneManagement; 
using System.Collections; 


[RequireComponent(typeof(AudioSource))]
public class MainMenuManager : MonoBehaviour
{
    [Header("Sound Effect")]
    [Tooltip("Drag and drop your 'flipping' sound effect here")]
    public AudioClip pageTurnSound;


    private AudioSource audioSource;
    private bool isLoading = false; 

    void Awake()
    {

        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    // --- Function for the 'Next Page' Button ---
    public void OnNextPageClicked()
    {
        if (isLoading) return; 
        Debug.Log("Next Page button clicked. Loading scene: page1");

        StartCoroutine(LoadSceneAfterSound("page1"));
    }


    public void OnExitClicked()
    {
        if (isLoading) return;
        Debug.Log("Exit button clicked. Quitting application...");

        StartCoroutine(QuitAfterSound());
    }


    private IEnumerator LoadSceneAfterSound(string sceneName)
    {
        isLoading = true;
        float delay = 0f;


        if (audioSource != null && pageTurnSound != null)
        {
            audioSource.PlayOneShot(pageTurnSound);
            delay = pageTurnSound.length; 
        }


        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }

    private IEnumerator QuitAfterSound()
    {
        isLoading = true;
        float delay = 0f;

        if (audioSource != null && pageTurnSound != null)
        {
            audioSource.PlayOneShot(pageTurnSound);
            delay = pageTurnSound.length;
        }


        yield return new WaitForSeconds(delay);

        Application.Quit();
    }
}
