using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;

public class HandController : NetworkBehaviour
{
    [Header("Arm Joints")]
    [SerializeField] private ConfigurableJoint upper;
    [SerializeField] private ConfigurableJoint lower;
    [SerializeField] private ConfigurableJoint hand;
    [Header("Other Objects")]
    [SerializeField] private GameObject player;
    [SerializeField] GameObject body;
    [SerializeField] private Transform handTransform;
    [Header("settings")]
    [SerializeField] private float l1 = 0.8f;
    [SerializeField] private float l2 = 0.6f;
    private ConfigurableJoint fixedJoint;
    private Vector3 desiredPos = new(0, -0.3f, 0);
    private bool grab = false;

    void Start()
    {
        // if (!IsHost)
        // {
        //     Destroy(upper);
        //     Destroy(lower);
        //     Destroy(hand);

        // }
    }

    void Update()
    {
        if (IsServer)
        {
            if (grab)
            {
                if (fixedJoint == null) TryGrab();
            }
            else if (fixedJoint != null)
            {
                if (fixedJoint.connectedBody.TryGetComponent(out Tool tool))
                    tool.Dropped(player);
                Destroy(fixedJoint);
                fixedJoint = null;
            }
            UpdateJoints();
        }
    }

    public void SetPostion(Vector3 pos)
    {
        if (pos.z < 0.3f)
            pos.z = 0.3f;
        if (pos.magnitude > l1 + l2 - 0.01f)
            desiredPos = pos.normalized * (l1 + l2 - 0.01f);
        else
            desiredPos = pos;
    }

    [ServerRpc]
    public void ShiftPostionServerRpc(Vector3 shift)
    {
        SetPostion(GetDesiredPostion() + shift);
    }

    public Vector3 GetPostion()
    {
        return transform.position;
    }

    public Vector3 GetDesiredPostion()
    {
        return desiredPos;
    }

    private void UpdateJoints()
    {
        Vector3 rotations = DoIK(GetDesiredPostion(), l1, l2);
        upper.targetRotation = quaternion.EulerZYX(-rotations.y, 0, rotations.x);
        lower.targetRotation = quaternion.EulerXYZ(rotations.z, 0, 0);
    }

    private Vector3 DoIK(Vector3 position, float L1, float L2)
    {
        return new()
        {
            x = (float)Math.Atan2(position.x, position.z),
            y = (float)
            (
                math.acos((Math.Pow(L1, 2) + Math.Pow(position.magnitude, 2) - Math.Pow(L2, 2)) / (2 * L1 * position.magnitude))
                + Math.Atan2(-position.y, Math.Sqrt(Math.Pow(position.x, 2) + Math.Pow(position.z, 2)))
            ),
            z = (float)(Math.PI - math.acos((Math.Pow(L1, 2) + Math.Pow(L2, 2) - Math.Pow(position.magnitude, 2)) / (2 * L1 * L2)))
        };
    }


    public Vector3 GetHandPos()
    {
        return handTransform.position;
    }

    private void TryGrab()
    {
        Collider[] colliders = Physics.OverlapSphere(GetHandPos(), 0.2f);
        GameObject closestGameObject = null;
        float closestColliderDistance = 100;
        foreach (Collider collider in colliders)
        {
            if (
                body == collider.gameObject ||
                upper.gameObject == collider.gameObject ||
                lower.gameObject == collider.gameObject ||
                hand.gameObject == collider.gameObject
                ) continue;

            GameObject gameObject = collider.gameObject;
            if (collider.gameObject.GetComponent<Rigidbody>() == null)
                if (collider.gameObject.transform.parent.TryGetComponent(out Rigidbody rigidbody))
                    gameObject = rigidbody.gameObject;
                else
                    continue;

            float dis = (collider.ClosestPoint(GetHandPos()) - GetHandPos()).magnitude;
            if (dis < closestColliderDistance)
            {
                closestGameObject = gameObject;
                closestColliderDistance = dis;
            }
        }
        if (closestGameObject != null)
        {
            fixedJoint = hand.AddComponent<ConfigurableJoint>();
            fixedJoint.anchor = handTransform.localPosition;
            fixedJoint.xMotion = ConfigurableJointMotion.Locked;
            fixedJoint.yMotion = ConfigurableJointMotion.Locked;
            fixedJoint.zMotion = ConfigurableJointMotion.Locked;
            fixedJoint.projectionMode = JointProjectionMode.PositionAndRotation;
            fixedJoint.connectedBody = closestGameObject.GetComponent<Rigidbody>();
            if (closestGameObject.TryGetComponent(out Tool tool))
            {
                fixedJoint.angularXMotion = ConfigurableJointMotion.Locked;
                fixedJoint.angularYMotion = ConfigurableJointMotion.Locked;
                fixedJoint.angularZMotion = ConfigurableJointMotion.Locked;
                tool.Grabbed(player);
            }
        }
        else
        {
            grab = false;
        }
    }

    [ServerRpc]
    public void ToggleGrabServerRpc()
    {
        grab = !grab;
    }
}
