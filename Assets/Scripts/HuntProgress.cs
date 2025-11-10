using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

public class HuntProgress : MonoBehaviour
{
    public static HuntProgress Instance { get; private set; }
    public List<string> scavengerScenes = new List<string> { "Entrance", "EntranceHallway", "Sideroom", "BackLeft", "BackMiddle", "BackRight", "FrontClose", "FrontFar", "FrontMain", "Staircase", "absorptionLab", "distillationLab", "reactionLab", "refrigerationLab" };
    private HashSet<string> visited = new HashSet<string>();

    public Slider progressBar; // Assign in Inspector
    void UpdateProgressBar()
    {
        progressBar.maxValue = HuntProgress.Instance.TotalScenes();
        progressBar.value = HuntProgress.Instance.VisitedCount();
    }

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); return;
        }
        Instance = this;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scavengerScenes.Contains(scene.name))
            visited.Add(scene.name);

        UpdateProgressBar();
    }

    public bool IsSceneVisited(string sceneName) => visited.Contains(sceneName);
    public int VisitedCount() => visited.Count;
    public int TotalScenes() => scavengerScenes.Count;
}
