using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : NetworkBehaviour
{

    [SerializeField] private Rigidbody player;
    private Quaternion desiredRotation;

    [Header("Sensitivity")]
    [SerializeField] private float torque;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private InputActionReference rot2dAxisLeft;
    [SerializeField] private InputActionReference rot2dAxisRight;
    [SerializeField] private InputActionReference rP;
    [SerializeField] private InputActionReference rN;
    [SerializeField] private Transform vrCam;
    [SerializeField] private Transform vrRig;


    public override void OnNetworkSpawn()
    {
        GetComponentInChildren<Camera>().gameObject.SetActive(IsOwner);
        desiredRotation.x = transform.rotation.x;
        desiredRotation.y = transform.rotation.y;
        desiredRotation.z = transform.rotation.z;
        desiredRotation.w = transform.rotation.w;
    }

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    public bool CursorLocked = false;

    public void Update()
    {
        if (IsClient)
        {
            if (CursorLocked)
            {
                if (Cursor.lockState == CursorLockMode.None)
                    Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                if (Cursor.lockState == CursorLockMode.Locked)
                    Cursor.lockState = CursorLockMode.None;
            }
        }

        if (IsServer)
        {
            Quaternion rotationDifference = vrCam.rotation * quaternion.EulerXYZ(math.PI/2, 0, 0) * Quaternion.Inverse(player.rotation);
            rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);
            player.AddTorque(Time.deltaTime * torque * angle * 0.4f * axis);
        }
        if (!IsOwner) return;

        vrRig.localRotation = Quaternion.Inverse(player.rotation) * desiredRotation;

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

        AddToDesiredRotationServerRpc(Time.deltaTime * rotationSpeed * (rot2dAxisLeft.action.ReadValue<Vector2>().y + rot2dAxisRight.action.ReadValue<Vector2>().y) * Vector3.left);
        AddToDesiredRotationServerRpc(Time.deltaTime * rotationSpeed * (rot2dAxisLeft.action.ReadValue<Vector2>().x + rot2dAxisRight.action.ReadValue<Vector2>().x) * Vector3.back);
    }

    [ServerRpc]
    private void AddToDesiredRotationServerRpc(Vector3 rotation)
    {
        desiredRotation *= Quaternion.Euler(vrCam.transform.localRotation * rotation);
        // if (TryGetComponent(out ArmMovement armMovement))
        // {
        //     if (armMovement.GetLeft().IsHolding() && !armMovement.GetLeft().IsFixed())
        //     {
        //         armMovement.GetLeft().RotationBy(Quaternion.Inverse(Quaternion.Euler(rotation)));
        //     }
        //     if (armMovement.GetRight().IsHolding() && !armMovement.GetRight().IsFixed())
        //     {
        //         armMovement.GetRight().RotationBy(Quaternion.Inverse(Quaternion.Euler(rotation)));
        //     }
        // }
    }
}

