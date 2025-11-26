using UnityEngine;
using UnityEngine.Video;
using System.IO;

public class VideoPlayerScript : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string fileName = "myFile.mp4";

    void Start()
    {
        Debug.Log("Filename = '" + fileName + "' Length = " + fileName.Length);
        string url = System.IO.Path.Combine(Application.streamingAssetsPath, fileName);
        Debug.Log("URL = " + url);
        videoPlayer.url = url;
        videoPlayer.Play();
    }

}
