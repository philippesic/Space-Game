using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    float xRotation;
    float yRotation;

    [Header("Sensitivity")]
    public float sensX;
    public float sensY;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // void Update()
    // {
    //     float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * sensX;
    //     float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * sensY;

    //     yRotation += mouseX;
    //     xRotation -= mouseY;

    //     xRotation = Mathf.Clamp(xRotation, -90f, 90f);
    //     transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
    // }
}