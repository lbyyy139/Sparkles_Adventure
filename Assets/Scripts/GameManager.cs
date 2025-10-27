

using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class GameManager : MonoBehaviour
{
    [Header("Setup")]
    [Tooltip("【 Important 】 The file name of your main menu scene (must match exactly)")]
    public string mainMenuSceneName = "SampleScene";

    [Tooltip("【 Important 】 The file name of your main menu scene (must match exactly)")]
    public string fadeScreenObjectName = "FadeScreen";

    [Header("Background Music")]
    [Tooltip("Drag and drop your background music (MP3/WAV) here")]
    public AudioClip backgroundMusic;

    [Tooltip("Adjust the volume of background music in the Inspector")]
    [Range(0f, 1f)]
    public float musicVolume = 0.5f;

    [Header("Scene Fade-In")]
    [Tooltip("How long does it take to fade from the black field to the scene")]
    public float fadeDuration = 1.5f;


    public static GameManager instance;


    private AudioSource audioSource;
    private Image fadeScreen; 

    void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);


            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }


        audioSource = GetComponent<AudioSource>();
        audioSource.clip = backgroundMusic;
        audioSource.volume = musicVolume;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
    }

    void Start()
    {

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }


    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == mainMenuSceneName)
        {

            GameObject fadeScreenObj = GameObject.Find(fadeScreenObjectName);

            if (fadeScreenObj != null)
            {

                fadeScreen = fadeScreenObj.GetComponent<Image>();


                fadeScreen.gameObject.SetActive(true);
                fadeScreen.color = Color.black;


                StartCoroutine(FadeIn());
            }
            else
            {

                Debug.LogWarning("GameManager: No object named '+fadeScreenObject Name+' was found in the '+scene. name+' scene.");
            }
        }
    }


    private IEnumerator FadeIn()
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float smoothAlpha = Mathf.Lerp(1f, 0f, timer / fadeDuration);
            fadeScreen.color = new Color(0, 0, 0, smoothAlpha);
            yield return null;
        }

        fadeScreen.color = Color.clear; 
        fadeScreen.gameObject.SetActive(false); 
    }


    void OnValidate()
    {
        if (audioSource != null)
        {
            audioSource.volume = musicVolume;
        }
    }


    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
