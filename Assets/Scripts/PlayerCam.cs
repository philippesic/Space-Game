using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    float xRotation;
    float yRotation;
    float zRotation;

    private GameObject player;

    [Header("Sensitivity")]
    public float sensX;
    public float sensY;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    {

        player.transform.rotation = Quaternion.Euler(xRotation, yRotation, zRotation);

        if (Input.GetKey(KeyCode.Q))
        {
            zRotation += 1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            zRotation -= 1f;
        }
        if (Input.GetKey(KeyCode.W))
        {
            xRotation -= 1f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            xRotation += 1f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            yRotation -= 1f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            yRotation += 1f;
        }

    }
}