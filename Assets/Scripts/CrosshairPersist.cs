using UnityEngine;

public class CrosshairPersist : MonoBehaviour
{
    private static CrosshairPersist instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // keeps it between scenes
        }
        else
        {
            Destroy(gameObject); // prevents duplicates
        }
    }
}
