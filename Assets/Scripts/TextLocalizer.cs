using TMPro;
using UnityEngine;

public class TextLocalizer : MonoBehaviour
{
    [SerializeField] private string key;
    [SerializeField] private TextMeshProUGUI label;

    void OnEnable() { Refresh(); }
    public void Refresh()
    {
        if (LangSystem.Instance == null || label == null) return;
        label.text = LangSystem.Instance.GetText(key);
    }
}
