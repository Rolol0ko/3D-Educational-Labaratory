using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject panelRoot;      // Assign InfoPanel

    // Copied from arrow hotspot script
    private Camera cam;

    void Start()
    {
        cam = Camera.main;
        HideInfoPanel();
    }

    // Show info panel
    public void ShowInfoPanel()
    {
        panelRoot.SetActive(true);
    }
    // Hide info panel
    public void HideInfoPanel()
    {
        panelRoot.SetActive(false);
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
