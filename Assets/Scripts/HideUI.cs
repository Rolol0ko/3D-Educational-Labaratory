using UnityEngine;

public class HideUI : MonoBehaviour
{
    public GameObject inGameUI;

    void OnMouseDown()
    {
        inGameUI.SetActive(false);
        Debug.Log("Hid UI");
    }
}
