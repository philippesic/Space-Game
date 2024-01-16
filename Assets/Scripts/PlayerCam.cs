using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public Rigidbody player;
    Quaternion desiredRotation;
    Quaternion currentRotation;

    [Header("Sensitivity")]
    public float torque;


    public void Update()
    {
        currentRotation = Quaternion.Euler(player.rotation.eulerAngles);
        if (Input.GetKey(KeyCode.Q))
        {
            ApplyTorque(Vector3.up);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            ApplyTorque(Vector3.down);
        }
        if (Input.GetKey(KeyCode.S))
        {
            ApplyTorque(Vector3.right);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            ApplyTorque(Vector3.left);
        }
        if (Input.GetKey(KeyCode.A))
        {
            ApplyTorque(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyTorque(Vector3.back);
        }
        if (!(Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)))
        {
            if (
                Input.GetKeyUp(KeyCode.Q) ||
                Input.GetKeyUp(KeyCode.E) ||
                Input.GetKeyUp(KeyCode.S) ||
                Input.GetKeyUp(KeyCode.W) ||
                Input.GetKeyUp(KeyCode.A) ||
                Input.GetKeyUp(KeyCode.D)
                )
            {
                desiredRotation.x = player.rotation.x;
                desiredRotation.y = player.rotation.y;
                desiredRotation.z = player.rotation.z;
                desiredRotation.w = player.rotation.w;
            }
            Quaternion rotationDifference = desiredRotation * Quaternion.Inverse(player.rotation);
            rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);
            player.AddTorque(axis * angle * torque);
        }
    }

    void ApplyTorque(Vector3 axis)
    {
        player.AddRelativeTorque(axis * torque);
    }
}

