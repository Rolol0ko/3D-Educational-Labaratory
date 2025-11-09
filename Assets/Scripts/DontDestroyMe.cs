using UnityEngine;

public class UserSingleton : MonoBehaviour
{
    private static UserSingleton instance;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes
    }
}