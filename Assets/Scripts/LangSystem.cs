using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class LocalizationData
{
    //This is where to add more text instances (also add them to to the json)
    public string MainMenuTitle;
    public string PlayButton;
    public string OptionsButton;
    public string QuitButton;
    public string LanguageButton;
    public string DistillationLab;
    public string FiltrationLab;
    public string BackButton;
}

public class LangSystem : MonoBehaviour
{
    public static LangSystem Instance { get; private set; }
    public LocalizationData LoadedData { get; private set; }
    public string currentFile = "english"; // just "english", no .json

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        LoadJsonFile(currentFile);
    }

    public void LoadJsonFile(string filename)
    {
        TextAsset jsonFile = Resources.Load<TextAsset>(filename);
        if (jsonFile == null)
        {
            Debug.LogError("JSON file not found: " + filename);
            return;
        }
        LoadedData = JsonUtility.FromJson<LocalizationData>(jsonFile.text);
    }

    public string GetText(string key)
    {
        if (LoadedData == null)
            return $"[{key} missing]";
        // Using reflection to get field by name
        var field = typeof(LocalizationData).GetField(key);
        if (field != null)
        {
            var val = field.GetValue(LoadedData) as string;
            return val ?? $"[{key} missing]";
        }
        return $"[{key} missing]";
    }

    public void SwitchFile(string filename)
    {
        currentFile = filename;
        LoadJsonFile(filename);
        TextLocalizer.NotifyAll();
    }
}
