using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChecklistUI : MonoBehaviour
{
    [System.Serializable]
    public class Row
    {
        public GameObject root;
        public TextMeshProUGUI label;
        public GameObject checkmark;
    }

    [SerializeField] private List<Row> rows = new(); // assign 3 rows in the Inspector
    const string TAG = "[ChecklistUI]";

    void OnEnable()
    {
        Debug.Log($"{TAG} Enabled on {name}", this);
        Refresh();
    }

    void OnDisable()
    {
        Debug.Log($"{TAG} Disabled on {name}", this);
    }

    public static void RefreshAll()
    {
        // Unity 6 replacement for deprecated API; no sorting for performance
        var list = Object.FindObjectsByType<ChecklistUI>(
            FindObjectsInactive.Exclude,
            FindObjectsSortMode.None
        );
        Debug.Log($"[ChecklistUI] RefreshAll found {list.Length} panels");
        foreach (var ui in list) ui.Refresh();
    }

    public void Refresh()
    {
        if (ChecklistManager.Instance == null)
        {
            Debug.LogWarning($"{TAG} No ChecklistManager present, skipping refresh", this);
            return;
        }

        var visible = new List<string>(ChecklistManager.Instance.GetVisibleTasks(3));
        Debug.Log($"{TAG} Refresh on {name} | Visible={string.Join(", ", visible)}", this);

        for (int i = 0; i < rows.Count; i++)
        {
            if (i < visible.Count)
            {
                string task = visible[i];
                // before: rows[i].label.text = task;  // task == scene name
                string key = $"Checklist.{task}";
                string display = LangSystem.Instance != null ? LangSystem.Instance.GetText(key) : task;
                rows[i].label.text = string.IsNullOrEmpty(display) ? task : display;  // fallback to scene name
                Debug.Log($"{TAG} Row{i}: text='{task}' completed={ChecklistManager.Instance.IsCompleted(task)}", this);
            }
            else
            {
                rows[i].root.SetActive(false);
                Debug.Log($"{TAG} Row{i}: hidden", this);
            }
        }
    }
}
