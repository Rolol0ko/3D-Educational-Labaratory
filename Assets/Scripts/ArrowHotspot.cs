using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ArrowHotspot : MonoBehaviour
{
    [Tooltip("Invoked when this arrow is clicked")]
    public UnityEvent onClicked;

    private Camera cam;

    void Start()
    {
        cam = Camera.main;
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
                    onClicked.Invoke(); // your teammate can hook into this
                }
            }
        }
    }
}