using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{

    public Rigidbody player;
    Quaternion playerRotation;
    Quaternion currentRotation;

    [Header("Sensitivity")]
    public float torque;


    void Update()
    {

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

    }
    void ApplyTorque(Vector3 axis)
    {
        // ArticulationDrive drive = player.xDrive;

        // drive.target = Mathf.Sign(axis.x) * torque;
        // drive.forceLimit = torque;

        // player.xDrive = drive;

        player.AddRelativeTorque(axis * torque);
    }
}
