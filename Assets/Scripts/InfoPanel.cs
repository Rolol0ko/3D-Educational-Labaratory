using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject panelRoot;      // Assign InfoPanel

    private CameraRotation look;

    // Copied from arrow hotspot script
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        if (UserSingleton.instance != null) look = UserSingleton.instance.Look;
        else Debug.LogWarning("User Singleton not found");
        HideInfoPanel();
    }

    // Show info panel
    public void ShowInfoPanel()
    {
        panelRoot.SetActive(true);
        look.SetMovementActive(false); //wha
    }
    // Hide info panel
    public void HideInfoPanel()
    {
        panelRoot.SetActive(false);
        look.SetMovementActive(true); //wha
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    ShowInfoPanel();
                    Debug.Log("Info Panel Button Clicked");
                }
            }
        }
    }
}
