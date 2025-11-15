using UnityEngine;

public class UserSingleton : MonoBehaviour
{
    public static UserSingleton instance { get; private set; }

    [SerializeField] public CameraRotation cameraRotation;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject); // Persist across scenes

        if (cameraRotation == null) cameraRotation = GetComponentInChildren<CameraRotation>(true);
    }
    public CameraRotation Look => cameraRotation;
}