using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Entry { public string key; public string value; }   // compatible with JsonUtility
[Serializable]
public class LocalizationList { public List<Entry> entries = new(); }

public class LangSystem : MonoBehaviour
{
    public static LangSystem Instance { get; private set; }
    public string currentFile = "english"; // Resources/english.json
    private readonly Dictionary<string, string> table = new Dictionary<string, string>(StringComparer.Ordinal);

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadJsonFile(currentFile);
    }

    public void LoadJsonFile(string filename)
    {
        table.Clear();
        var ta = Resources.Load<TextAsset>(filename);
        if (ta == null) { Debug.LogError($"LangSystem: file not found '{filename}'"); return; }

        // Try flat object first; if it fails, fallback to entries[]
        try
        {
            // Flat object: {"PlayButton":"Begin", ...}
            var flat = JsonUtility.FromJson<Wrapper>(Wrap(ta.text));
            if (flat != null && flat.dict != null)
            {
                foreach (var kv in flat.dict) table[kv.Key] = kv.Value;
                return;
            }
        }
        catch { /* fall back */ }

        // Fallback: entries array: {"entries":[{"key":"PlayButton","value":"Begin"}, ...]}
        var list = JsonUtility.FromJson<LocalizationList>(ta.text);
        if (list != null && list.entries != null)
            foreach (var e in list.entries)
                if (!string.IsNullOrEmpty(e.key)) table[e.key] = e.value ?? string.Empty;
    }

    // Access
    public string GetText(string key) => table.TryGetValue(key, out var v) ? v : $"[{key} missing]";

    // Dynamic adjust/add at runtime
    public void SetText(string key, string value) { if (!string.IsNullOrEmpty(key)) table[key] = value ?? string.Empty; }

    // Switch language/file at runtime
    public void SwitchFile(string filename) { currentFile = filename; LoadJsonFile(currentFile); /* notify UI here */ }

    // Helpers to parse flat JSON with JsonUtility (which needs fields)
    [Serializable] private class KV { public string k; public string v; }
    [Serializable] private class Wrapper { public List<KV> items; public Dictionary<string, string> dict; }
    private string Wrap(string json)
    {
        // Convert a flat object into a shape JsonUtility can parse:
        // {"A":"x","B":"y"} -> {"items":[{"k":"A","v":"x"},{"k":"B","v":"y"}]}
        // Then build dict after FromJson (Unity won't map directly to Dictionary)
        var dict = new Dictionary<string, string>();
        // Note: For production, prefer a robust JSON parser; see notes below.
        // Placeholder wrapper to satisfy JsonUtility; you can replace with a better parser.
        return "{\"entries\":[]}";
    }
}
