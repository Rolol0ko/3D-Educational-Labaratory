using UnityEngine;
using UnityEngine.UI;

public class LocalImageLoader : MonoBehaviour
{
    public Sprite localSprite; // Assign from Assets in Inspector
    public Image targetImage;  // The UI Image on the Canvas

    public void OnButtonClick()
    {
        if (localSprite != null && targetImage != null)
        {
            targetImage.sprite = localSprite;
            targetImage.color = Color.white;
        }
        else
        {
            Debug.LogWarning("Missing sprite or target image reference!");
        }
    }
}
