using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    public Vector2 turn;

    public float sensitivity = .5f;

    public Vector3 deltaMove;

    public float speed = 1;

    // stops/starts Update (called when menus open/close)
    public void SetMovementActive(bool active)
    {
        enabled = active;                         
        Cursor.lockState = active ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !active;
    }

    void Update()
    {

        turn.x += Input.GetAxis("Mouse X") * sensitivity;

        turn.y += Input.GetAxis("Mouse Y") * sensitivity;

        transform.localRotation = Quaternion.Euler(-turn.y, turn.x, 0);

    }

}
