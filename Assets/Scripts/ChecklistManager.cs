using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChecklistManager : MonoBehaviour
{
    public static ChecklistManager Instance { get; private set; }
    public List<string> sceneTasks = new();          // keep ordered tasks here
    private HashSet<string> visited = new();
    public int displayStartIndex = 0;

    const string TAG = "[ChecklistManager]";

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Debug.LogWarning($"{TAG} Duplicate destroyed on {name}", this);
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        Debug.Log($"{TAG} Awake on {name}. Tasks={sceneTasks.Count}, Persisting across scenes", this);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        Debug.Log($"{TAG} Subscribed to sceneLoaded", this);
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        Debug.Log($"{TAG} Unsubscribed from sceneLoaded", this);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"{TAG} sceneLoaded: '{scene.name}' mode={mode}", this);
        if (sceneTasks.Contains(scene.name))
        {
            bool added = visited.Add(scene.name);
            Debug.Log($"{TAG} Mark visited: '{scene.name}' (added={added}) | Visited={visited.Count}/{sceneTasks.Count}", this);
        }
        AdvanceWindowIfAllVisibleCompleted();
        ChecklistUI.RefreshAll(); // ensure UI updates after scene visit
    }

    public bool IsCompleted(string sceneName) => visited.Contains(sceneName);

    public IEnumerable<string> GetVisibleTasks(int count = 3)
    {
        int shown = 0;
        List<string> snapshot = new();
        for (int i = displayStartIndex; i < sceneTasks.Count && shown < count; i++)
        {
            if (!visited.Contains(sceneTasks[i]))
            {
                snapshot.Add(sceneTasks[i]);
                shown++;
            }
        }
        Debug.Log($"{TAG} GetVisibleTasks start={displayStartIndex} -> [{string.Join(", ", snapshot)}]", this);
        return snapshot;
    }

    public void AdvanceWindowIfAllVisibleCompleted(int count = 3)
    {
        // Slide to first incomplete if current window has no incomplete items
        int before = displayStartIndex;
        if (NextIncompleteIndex(out int next))
        {
            displayStartIndex = next;
        }
        Debug.Log($"{TAG} AdvanceWindow: {before} -> {displayStartIndex}", this);
    }

    public bool NextIncompleteIndex(out int index)
    {
        for (int i = 0; i < sceneTasks.Count; i++)
            if (!visited.Contains(sceneTasks[i])) { index = i; return true; }
        index = sceneTasks.Count;
        return false;
    }

    public int VisitedCount() => visited.Count;
    public int TotalCount() => sceneTasks.Count;
}
