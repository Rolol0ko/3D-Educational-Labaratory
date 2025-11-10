using UnityEngine;

public class MenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        SceneController.Instance.FadeAndSwitchScene("Entrance"); // Switch to Entrance Scene
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
