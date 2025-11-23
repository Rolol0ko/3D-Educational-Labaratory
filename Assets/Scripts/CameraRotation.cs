using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public bool mouseMovementActive;

    private float X;
    private float Y;

    void Update()
    {
        if (Input.GetMouseButton(0) && mouseMovementActive)
        {
            transform.Rotate(new Vector3(Input.GetAxis("Mouse Y"), -Input.GetAxis("Mouse X"), 0));
            X = transform.rotation.eulerAngles.x;
            Y = transform.rotation.eulerAngles.y;
            transform.rotation = Quaternion.Euler(X, Y, 0);
        }
    }
}
