using UnityEngine;

public class MenuActions : MonoBehaviour
{
    public void PlayGame()
    {
        SceneController.Instance.FadeAndSwitchScene("Entrance"); // Switch to Entrance Scene
    }
}
