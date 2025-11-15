# 3D-Educational-Labaratory

A Unity project featuring a persistent JSON‑driven localization system, menus and UIs, a fade transition between scenes, a progress bar that updates on scene visits, and an Info Panel that displays localized context text.

## Features

- Scene navigation with fade and cursor state management that unlocks in menus and locks during gameplay.
- JSON‑based localization loaded via Resources, feeding TextMesh Pro UI via string keys instead of hardcoded text.
//- Scavenger‑hunt checklist that marks tasks complete when scenes load, with the display capped to three visible tasks at a time.

## Project Structure

- Assets/Scripts: Core gameplay, UI controllers, singleton, and localization helpers.
- Assets/Resources: JSON files per language or profile (e.g., english.json, french.json) loaded at runtime.
- Assets/Prefabs: Reusable UI prefabs such as the Info Panel and Checklist Item.

## Requirements

- Unity 2021 LTS or newer

## Getting Started

1) Clone the repository.
2) Open the project in a compatible Unity version.
3) Open the MainMenu scene, which is the "starting scene" for the project.

## Persistent User Singleton

- The User root GameObject sets itself as the global instance in Awake and calls DontDestroyOnLoad to persist across scenes.
- The Main Camera is a child of User; access it or its CameraRotation via GetComponentInChildren to avoid losing references between scenes.
- Any scene UI can safely fetch the camera or look controller at runtime from the singleton rather than relying on serialized scene references.

## JSON Localization (Resources + TMP)

- JSON files live under Assets/Resources and are loaded with Resources.Load<TextAsset>("english") without the .json extension.
- Data is parsed via Unity’s JSON serialization and stored in a Dictionary<string, string> for fast key lookups.
- Each TextMesh Pro label uses a small component (e.g., TextLocalizer) to request text by key on Start and when language changes.

/*
## Scavenger‑Hunt Checklist

- A persistent progress manager listens to SceneManager.sceneLoaded and marks the corresponding scene task complete upon load.
- The checklist UI shows at most three active tasks by filling three slots from the ordered list and skips completed items.
- Optionally use a ScrollView if you want to browse the full task list while maintaining a capped “featured” section of three items.
*/

## Multi‑Scene Workflow

- Uses a persistent core scene (User and managers) and load 360 content scenes additively, while still being able to load in on any scene from the editor.

## License

- MIT
