using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Task
{
    public string name;
    public bool isCompleted;
}

public class HuntProgress : MonoBehaviour
{
    public static HuntProgress Instance { get; private set; }
    public List<string> scavengerScenes = new List<string> { "Entrance", "EntranceHallway", "Sideroom", "BackLeft", "BackMiddle", "BackRight", "FrontClose", "FrontFar", "FrontMain", "Staircase", "absorptionLab", "distillationLab", "reactionLab", "refrigerationLab" };
    private HashSet<string> visited = new HashSet<string>();

    public Slider progressBar; // Assign in Inspector

    [SerializeField] private List<Task> tasks = new List<Task>();
    private int displayStartIndex = 0;

    void TryAdvanceDisplay()
    {
        // Check if all currently visible tasks are completed
        bool allCompleted = true;
        int countShown = 0;
        int idx = displayStartIndex;

        while (countShown < 3 && idx < tasks.Count)
        {
            if (!tasks[idx].isCompleted)
            {
                allCompleted = false;
                break;
            }
            idx++;
            countShown++;
        }

        if (allCompleted)
        {
            // Move display start to next uncompleted or end
            while (displayStartIndex < tasks.Count && tasks[displayStartIndex].isCompleted)
            {
                displayStartIndex++;
            }
        }
    }

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
