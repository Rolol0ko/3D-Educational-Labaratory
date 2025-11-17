using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class ChecklistUI : MonoBehaviour
{
    [SerializeField] private List<Row> rows; // size 3

    void OnEnable() { Refresh(); }

    public static void RefreshAll()
    {
        var list = Object.FindObjectsByType<ChecklistUI>(
            FindObjectsInactive.Exclude,          // or Include if you need inactive UIs
            FindObjectsSortMode.None              // faster; no InstanceID sorting
        );
        foreach (var ui in list) ui.Refresh();
    }

    public void Refresh()
    {
        if (ChecklistManager.Instance == null) return;

        var visible = new List<string>(ChecklistManager.Instance.GetVisibleTasks(3));
        for (int i = 0; i < rows.Count; i++)
        {
            if (i < visible.Count)
            {
                string task = visible[i];
                rows[i].root.SetActive(true);
                rows[i].label.text = task;
                rows[i].checkmark.SetActive(ChecklistManager.Instance.IsCompleted(task));
            }
            else
            {
                rows[i].root.SetActive(false);
            }
        }
    }

    [System.Serializable]
    public class Row
    {
        public GameObject root;
        public TextMeshProUGUI label;
        public GameObject checkmark;
    }
}
