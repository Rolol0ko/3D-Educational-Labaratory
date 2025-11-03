using UnityEngine;
using UnityEngine.SceneManagement;

public class TourStarter : MonoBehaviour
{
    public string nextscene = "Entrance";

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SceneManager.LoadSceneAsync(nextscene);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
