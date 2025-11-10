using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class TextLocalizer : MonoBehaviour
{
    public string textKey; // assign in Inspector
    private TextMeshProUGUI uiText;

    private static List<TextLocalizer> allLocalizers = new List<TextLocalizer>();

    void Awake()
    {
        uiText = GetComponent<TextMeshProUGUI>();
        allLocalizers.Add(this);
    }
    void Start()
    {
        UpdateText();
    }
    void OnDestroy()
    {
        allLocalizers.Remove(this);
    }

    public void UpdateText()
    {
        if (uiText != null && LangSystem.Instance != null)
            uiText.text = LangSystem.Instance.GetText(textKey);
    }

    // Allows LocalizationManager to notify all active localizers
    public static void NotifyAll()
    {
        foreach (var loc in allLocalizers) loc.UpdateText();
    }
}
