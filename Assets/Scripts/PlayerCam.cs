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


    public void FixedUpdate()
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
        StartCoroutine(UpdateRotation());

    }

    void ApplyTorque(Vector3 axis)
    {
        player.AddRelativeTorque(axis * torque);
    }
    public IEnumerator UpdateRotation()
    {
        if (Input.anyKey && !Input.GetKey(KeyCode.Mouse0) && !Input.GetKey(KeyCode.Mouse1))
        {
            desiredRotation.x = player.rotation.x;
            desiredRotation.y = player.rotation.y;
            desiredRotation.z = player.rotation.z;
            desiredRotation.w = player.rotation.w;
        }
        if (!Input.anyKey)
        {

            Quaternion rotationDifference = desiredRotation * Quaternion.Inverse(player.rotation);


            Vector3 axis;
            float angle;
            rotationDifference.ToAngleAxis(out angle, out axis);


            player.AddTorque(axis * angle * torque);
            yield return new WaitForSeconds(0.1f);
        }
    }
}

