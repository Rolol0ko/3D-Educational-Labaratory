using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject panelRoot;      // Assign InfoPanel

    // Copied from arrow hotspot script
    private Camera cam;
    private GameObject UI;

    void Start()
    {
        UI = GameObject.Find("UI");
        cam = Camera.main;
        HideInfoPanel();
    }

    // Show info panel
    public void ShowInfoPanel()
    {
        UI = GameObject.Find("UI");
        panelRoot.SetActive(true);
        UI.SetActive(false);
    }
    // Hide info panel
    public void HideInfoPanel()
    {
        panelRoot.SetActive(false);
        UI.SetActive(true);
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
