using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{

    [SerializeField] private Rigidbody player;
    private Quaternion desiredRotation;

    [Header("Sensitivity")]
    [SerializeField] private float torque;
    [SerializeField] private float torqueS;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private InputActionReference rot2dAxisLeft;
    [SerializeField] private InputActionReference rot2dAxisRight;
    [SerializeField] private InputActionReference rP;
    [SerializeField] private InputActionReference rN;
    [SerializeField] private Transform vrCam;
    [SerializeField] private Transform vrRig;

    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        desiredRotation = quaternion.identity;
    }

    public bool CursorLocked = false;

    public void Update()
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

        Quaternion rotationDifference = vrCam.rotation * quaternion.EulerXYZ(math.PI / 2, 0, 0) * Quaternion.Inverse(player.rotation);
        rotationDifference.ToAngleAxis(out float angle, out Vector3 axis);

        // Quaternion rotationDifferenceS = quaternion.identity * Quaternion.Inverse(player.angularVelocity);
        // rotationDifferenceS.ToAngleAxis(out float angleS, out Vector3 axisS);
        if (angle > 5)
            player.AddTorque(Time.deltaTime * (torque * 0.2f * angle * axis - torqueS * 0.3f * player.angularVelocity));
        else
            player.AddTorque(Time.deltaTime * (torque * 0.2f * angle * axis - torqueS * 0.3f * player.angularVelocity));
        vrRig.localRotation = Quaternion.Inverse(player.rotation) * desiredRotation;

        if (Input.GetKey(KeyCode.Q))
        {
            AddToDesiredRotationSRNOT(Time.deltaTime * rotationSpeed * Vector3.up);
        }
        else if (Input.GetKey(KeyCode.E))
        {
            AddToDesiredRotationSRNOT(Time.deltaTime * rotationSpeed * Vector3.down);
        }
        if (Input.GetKey(KeyCode.S))
        {
            AddToDesiredRotationSRNOT(Time.deltaTime * rotationSpeed * Vector3.right);
        }
        else if (Input.GetKey(KeyCode.W))
        {
            AddToDesiredRotationSRNOT(Time.deltaTime * rotationSpeed * Vector3.left);
        }
        if (Input.GetKey(KeyCode.A))
        {
            AddToDesiredRotationSRNOT(Time.deltaTime * rotationSpeed * Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            AddToDesiredRotationSRNOT(Time.deltaTime * rotationSpeed * Vector3.back);
        }

        AddToDesiredRotationSRNOT(Time.deltaTime * rotationSpeed * (rot2dAxisLeft.action.ReadValue<Vector2>().y + rot2dAxisRight.action.ReadValue<Vector2>().y) * Vector3.left);
        AddToDesiredRotationSRNOT(Time.deltaTime * rotationSpeed * (rot2dAxisLeft.action.ReadValue<Vector2>().x + rot2dAxisRight.action.ReadValue<Vector2>().x) * Vector3.back);
    }


    private void AddToDesiredRotationSRNOT(Vector3 rotation)
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

