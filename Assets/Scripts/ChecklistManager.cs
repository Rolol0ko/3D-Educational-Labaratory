using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChecklistManager : MonoBehaviour
{
    public static ChecklistManager Instance { get; private set; }

    [Tooltip("Ordered list of scene names you want to track")]
    public List<string> sceneTasks = new() { "Entrance", "EntranceHallway", "Sideroom", "BackLeft", "BackMiddle", "BackRight", "FrontClose", "FrontFar", "FrontMain", "Staircase", "absorptionLab", "distillationLab", "reactionLab", "refrigerationLab" };

    private HashSet<string> visited = new();
    public int displayStartIndex = 0; // first visible task index

    void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; }
    void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; }

    private void Awake()
    {
        Debug.Log("Checklist Manager Loaded");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (sceneTasks.Contains(scene.name))
            visited.Add(scene.name);

        AdvanceWindowIfAllVisibleCompleted();
        ChecklistUI.RefreshAll();
        Debug.Log("OnSceneLoaded called");
        Debug.Log($"{scene.name}");
    }

    public bool IsCompleted(string sceneName) => visited.Contains(sceneName);

    public IEnumerable<string> GetVisibleTasks(int count = 3)
    {
        int shown = 0;
        for (int i = displayStartIndex; i < sceneTasks.Count && shown < count; i++)
        {
            if (!IsCompleted(sceneTasks[i]))
            {
                yield return sceneTasks[i];
                shown++;
            }
        }
    }

    public void AdvanceWindowIfAllVisibleCompleted(int count = 3)
    {
        // Move the window to the next incomplete task if current window has none
        if (NextIncompleteIndex(out int next))
            displayStartIndex = next;
    }

    public bool NextIncompleteIndex(out int index)
    {
        for (int i = 0; i < sceneTasks.Count; i++)
            if (!visited.Contains(sceneTasks[i])) { index = i; return true; }
        index = sceneTasks.Count; return false;
    }

    public int VisitedCount() => visited.Count;
    public int TotalCount() => sceneTasks.Count;
}
