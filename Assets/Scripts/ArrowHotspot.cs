using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ArrowHotspot : MonoBehaviour
{
    //[Tooltip("Invoked when this arrow is clicked")]
    //public UnityEvent onClicked;

    public string m_NextScene = "TargetScene";

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
                    //onClicked.Invoke(); // your teammate can hook into this
                    /*
                    GameObject user = GameObject.Find("User");
                    if (user != null)
                    {
                        SceneController sc = user.GetComponentInChildren<SceneController>();
                        if (sc != null)
                        {
                            sc.ChangeScene("TargetScene");
                        }
                    }
                    else
                    {
                        Debug.LogError("SceneController not found!");
                    }
                    */

                    SceneController.Instance.FadeAndSwitchScene(m_NextScene);

                    Debug.Log("Button Clicked");
                }
            }
        }
    }
}