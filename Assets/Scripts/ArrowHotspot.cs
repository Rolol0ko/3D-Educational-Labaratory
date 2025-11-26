using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class ArrowHotspot : MonoBehaviour
{
    //[Tooltip("Invoked when this arrow is clicked")]
    //public UnityEvent onClicked;

    public string m_NextScene = "TargetScene";

    private Camera cam;
    private Animator anim;
    private bool isHovering = false;

    void Start()
    {
        cam = Camera.main;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        Onclick();
        Hover();
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