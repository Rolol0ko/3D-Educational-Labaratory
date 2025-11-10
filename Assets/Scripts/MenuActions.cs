using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtons : MonoBehaviour
{
    public void PlayGame()
    {
        SceneController.Instance.FadeAndSwitchScene("Entrance"); // Replace with your game scene name
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void OpenOptions()
    {
        // Show your options panel, or load an options scene
        Debug.Log("Options clicked");
    }
}
