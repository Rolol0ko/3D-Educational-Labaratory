using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    [SerializeField] private GameObject panelRoot;      // Assign InfoPanel

    // Copied from arrow hotspot script
    private Camera cam;
    private GameObject UI;
    private Animator anim;
    private bool isHovering = false;

    void Start()
    {
        UI = GameObject.Find("UI");
        cam = Camera.main;
        anim = GetComponent<Animator>();
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
        Hover();
        Onclick();
    }

    private void Onclick()
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

    private void Hover()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject == gameObject)
            {
                if (!isHovering)
                {
                    isHovering = true;
                    anim.SetBool("Hover", true);
                }
                return;
            }
        }
        if (isHovering)
        {
            isHovering = false;
            anim.SetBool("Hover", false);
        }
    }
}
