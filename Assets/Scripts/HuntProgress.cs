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

    public List<GameObject> checklistUISlots; // assign 3 UI prefabs

    void UpdateChecklist()
    {
        int shown = 0;
        int index = displayStartIndex;

        while (shown < 3 && index < tasks.Count)
        {
            if (!tasks[index].isCompleted)
            {
                // Update UI slot for this task
                checklistUISlots[shown].SetActive(true);
                checklistUISlots[shown].GetComponentInChildren<Text>().text = tasks[index].name;
                checklistUISlots[shown].transform.Find("Checkmark").gameObject.SetActive(tasks[index].isCompleted);
                shown++;
            }
            else
            {
                // skip completed tasks
            }
            index++;
        }

        // Hide any leftover UI slots if fewer than 3 remain
        for (int i = shown; i < 3; i++)
        {
            checklistUISlots[i].SetActive(false);
        }
    }

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
            UpdateChecklist();
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
