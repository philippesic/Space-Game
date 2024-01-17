using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class PlayerCam : NetworkBehaviour
{

    [SerializeField] private Rigidbody player;
    private Quaternion desiredRotation;

    [Header("Sensitivity")]
    [SerializeField] private float torque;
    [SerializeField] private float rotationSpeed;

    public override void OnNetworkSpawn()
    {
        GetComponentInChildren<Camera>().gameObject.SetActive(IsOwner);
        desiredRotation.x = transform.rotation.x;
        desiredRotation.y = transform.rotation.y;
        desiredRotation.z = transform.rotation.z;
        desiredRotation.w = transform.rotation.w;
    }

    public void Update()
    {
        if (IsServer)
        {
            Quaternion rotationDifference = desiredRotation * Quaternion.Inverse(player.rotation);
            rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);
            player.AddTorque(Time.deltaTime * torque * angle * 0.4f * axis);
        }
        if (!IsOwner) return;

        if (Input.GetKey(KeyCode.Q))
        {
            AddToDesiredRotationServerRpc(Time.deltaTime * rotationSpeed * Vector3.up);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            AddToDesiredRotationServerRpc(Time.deltaTime * rotationSpeed * Vector3.down);
        }
        if (Input.GetKey(KeyCode.S))
        {
            AddToDesiredRotationServerRpc(Time.deltaTime * rotationSpeed * Vector3.right);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            AddToDesiredRotationServerRpc(Time.deltaTime * rotationSpeed * Vector3.left);
        }
        if (Input.GetKey(KeyCode.A))
        {
            AddToDesiredRotationServerRpc(Time.deltaTime * rotationSpeed * Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            AddToDesiredRotationServerRpc(Time.deltaTime * rotationSpeed * Vector3.back);
        }
    }

    [ServerRpc]
    private void AddToDesiredRotationServerRpc(Vector3 rotation)
    {
        desiredRotation *= Quaternion.Euler(rotation);
    }
}

