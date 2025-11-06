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
<<<<<<< HEAD
                    onClicked.Invoke(); 
=======
                    onClicked.Invoke(); // your teammate can hook into this
                    Debug.Log("Button Clicked");
>>>>>>> 508901cd792f952c607e47da4a3066dd4a5dc386
                }
            }
        }
    }
}