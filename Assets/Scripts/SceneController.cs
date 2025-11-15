using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{   public static SceneController Instance { get; private set; }

    public Image fadeImage;
    public float fadeDuration = 1f;

    public GameObject inGameUI;

    [SerializeField] private CameraRotation look;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); return;
        }
        Instance = this;
    }

    public void FadeAndSwitchScene(string sceneName)
    {
        StartCoroutine(FadeInAndSwitch(sceneName));
    }

    IEnumerator FadeInAndSwitch(string sceneName)
    {
        // Fade to black
        yield return StartCoroutine(Fade(0f, 1f));
        // Switch scene
        SceneManager.LoadScene(sceneName);
        // Fade from black (if persistent object)
        yield return StartCoroutine(Fade(1f, 0f));
    }

    IEnumerator Fade(float startAlpha, float endAlpha)
    {
        float elapsed = 0f;
        Color c = fadeImage.color;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / fadeDuration);
            c.a = Mathf.Lerp(startAlpha, endAlpha, t);
            fadeImage.color = c;
            yield return null;
        }
        c.a = endAlpha;
        fadeImage.color = c;
    }

    // Subscribe to scene loaded events
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called every time a scene loads
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Main Menu")
        {
            inGameUI.SetActive(false);
            look.SetMovementActive(false);
        }
        else
        {
            inGameUI.SetActive(true);
            look.SetMovementActive(true);
        }
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
        Debug.Log(sceneName);
    }
}
