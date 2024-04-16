using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.AI;
using UnityEngine.InputSystem;

public class HandController : MonoBehaviour
{
    [Header("Arm Joints")]
    [SerializeField] private ConfigurableJoint upper;
    [SerializeField] private ConfigurableJoint lower;
    [SerializeField] private ConfigurableJoint hand;
    [Header("Other Objects")]
    [SerializeField] private Transform armBase;
    [SerializeField] private GameObject player;
    [SerializeField] GameObject body;
    [SerializeField] Player playerScript;
    [SerializeField] private Transform handTransform;
    [Header("settings")]
    [SerializeField] private float l1 = 0.8f;
    [SerializeField] private float l2 = 0.6f;
    [SerializeField] private Material notGrabbingMaterial;
    [SerializeField] private Material canGrabMaterial;
    [SerializeField] private Material grabbingMaterial;
    [SerializeField] private float trackSpeed = 1;
    [SerializeField] private GameObject ControllerDirection;
    private ConfigurableJoint fixedJoint;
    private Vector3 desiredPos = new(0, -0.3f, 0);
    private Vector3 trackingPos = new();
    private Quaternion desiredRotation = new();
    private bool grab = false;
    private bool isHoldingStatic = false;
    [SerializeField] InputActionReference useTool;
    void Update()
    {
        if (TryGetComponent(out ConfigurableJoint joint))
        {
            // Vector3 armF = lower.transform.rotation * Vector3.forward;
            // Vector3 armR = lower.transform.rotation * Vector3.right;
            // Vector3 armU = lower.transform.rotation * Vector3.up;
            // Vector3 handF = desiredRotation * Vector3.forward;
            // Vector3 handR = desiredRotation * Vector3.right;
            // Vector3 handU = desiredRotation * Vector3.up;
            // Vector3.ProjectOnPlane(handF, armU);
            // Quaternion quaternion = *Quaternion.Inverse(desiredRotation);
            // joint.targetRotation = Quaternion.Euler(quaternion.eulerAngles.x, quaternion.eulerAngles.y, quaternion.eulerAngles.z);
            // ControllerDirection.transform.rotation = quaternion;
        }
        if (fixedJoint == null)
        {
            if (grab) TryGrab();
            else if (TryGrapCheck(out _))
            {
                hand.GetComponentInChildren<MeshRenderer>().material = canGrabMaterial;
            }
            else
            {
                hand.GetComponentInChildren<MeshRenderer>().material = notGrabbingMaterial;
            }
        }
        else if (!grab)
        {
            if (fixedJoint.connectedBody.constraints == RigidbodyConstraints.FreezeAll)
            {
                playerScript.isHoldingStaticCount--;
                isHoldingStatic = false;
            }
            hand.GetComponentInChildren<MeshRenderer>().material = notGrabbingMaterial;
            if (fixedJoint.connectedBody.TryGetComponent(out Part part))
            {
                part.Dropped(player);
                if (part.TryGetComponent(out Tool tool))
                {
                    tool.RemoveInput(useTool);
                }
            }
            Destroy(fixedJoint);
            fixedJoint = null;
        }
        else
        {
            if (isHoldingStatic)
            {
                if (fixedJoint.connectedBody.constraints != RigidbodyConstraints.FreezeAll)
                {
                    playerScript.isHoldingStaticCount--;
                    isHoldingStatic = false;
                }
            }
            else
            {
                if (fixedJoint.connectedBody.constraints == RigidbodyConstraints.FreezeAll)
                {
                    playerScript.isHoldingStaticCount++;
                    isHoldingStatic = true;
                }
            }
        }
        float g = (GetDesiredPostion() - trackingPos).magnitude;
        trackingPos += (GetDesiredPostion() - trackingPos).normalized * math.min(0.2f, Time.deltaTime * trackSpeed * g);
        // trackingPos = GetDesiredPostion();
        UpdateJoints();
        // }
        // else
        // {
        //     if (grab)
        //         hand.GetComponentInChildren<MeshRenderer>().material = grabbingMaterial;
        //     else if (TryGrapCheck(out _))
        //         hand.GetComponentInChildren<MeshRenderer>().material = canGrabMaterial;
        //     else
        //         hand.GetComponentInChildren<MeshRenderer>().material = notGrabbingMaterial;
        // }
    }

    public void SetPostion(Vector3 pos)
    {
        if (pos.z < 0.01f)
            pos.z = 0.01f;
        if (pos.magnitude > l1 + l2 - 0.01f)
            desiredPos = pos.normalized * (l1 + l2 - 0.01f);
        else
            desiredPos = pos;
    }

    public void SetRotation(Quaternion rotation)
    {
        desiredRotation = rotation;
    }


    public void SetPostionSRNOT(Vector3 pos)
    {
        SetPostion(pos);
    }


    public void SetRotationSRNOT(Quaternion rotation)
    {
        SetRotation(rotation);
    }

    public void ShiftPostion(Vector3 shift)
    {
        Vector3 currentDesiredPostion = GetDesiredPostion();
        Vector3 direction = currentDesiredPostion / currentDesiredPostion.z;
        Vector3 pos = (direction + new Vector3(shift.x, shift.y, 0)).normalized;
        if (pos.z < 0.01f)
            pos.z = 0.01f;
        pos.Normalize();
        SetPostion(pos * (currentDesiredPostion.magnitude + shift.z));
    }


    public void ShiftPostionSRNOT(Vector3 shift)
    {
        ShiftPostion(shift);
    }

    public Vector3 LocalToGlobal(Vector3 localPos)
    {
        return body.transform.rotation * new Vector3(localPos.x, localPos.z, -localPos.y) + armBase.position;
    }

    public Vector3 GlobalToLocal(Vector3 globalPos)
    {
        Vector3 pos = Quaternion.Inverse(body.transform.rotation) * (globalPos - armBase.position);
        return new(pos.x, -pos.z, pos.y);
    }


    public void RotationBy(Quaternion rotation)
    {
        SetPostion(rotation * GetDesiredPostion());
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
        Vector3 rotations = DoIK(trackingPos, l1, l2);
        upper.targetRotation = quaternion.EulerZYX(-rotations.y, 0, rotations.x);
        lower.targetRotation = quaternion.EulerXYZ(rotations.z, 0, 0);
    }

    private Vector3 DoIK(Vector3 position, float L1, float L2)
    {
        if (position.magnitude > L1 + L2)
            position *= (L1 + L2) / position.magnitude;
        else if (position.magnitude < math.abs(L1 - L2))
            position *= (math.abs(L1 - L2) + 0.01f) / position.magnitude;
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

    private bool TryGrapCheck(out GameObject closestGameObject)
    {
        Collider[] colliders = Physics.OverlapSphere(GetHandPos(), 0.2f);
        closestGameObject = null;
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
        return closestGameObject != null;
    }

    private void TryGrab()
    {
        if (TryGrapCheck(out GameObject closestGameObject))
        {
            if (Vector3.Distance(GlobalToLocal(GetHandPos()), GetDesiredPostion()) < 0.5)
            {
                hand.GetComponentInChildren<MeshRenderer>().material = grabbingMaterial;
                fixedJoint = hand.AddComponent<ConfigurableJoint>();
                fixedJoint.anchor = handTransform.localPosition;
                fixedJoint.xMotion = ConfigurableJointMotion.Locked;
                fixedJoint.yMotion = ConfigurableJointMotion.Locked;
                fixedJoint.zMotion = ConfigurableJointMotion.Locked;
                fixedJoint.projectionMode = JointProjectionMode.PositionAndRotation;
                Rigidbody rb = closestGameObject.GetComponent<Rigidbody>();
                fixedJoint.connectedBody = rb;
                SetPostion(GlobalToLocal(GetHandPos()));
                if (closestGameObject.TryGetComponent(out Part part))
                {
                    if (rb.constraints == RigidbodyConstraints.FreezeAll)
                    {
                        playerScript.isHoldingStaticCount++;
                        isHoldingStatic = true;
                    }
                    part.Grabbed(player);
                    if (closestGameObject.TryGetComponent(out Tool tool))
                    {
                        ToggleGripSRNOT();
                        tool.GiveInput(useTool);
                    }
                }
            }
        }
        else
        {
            if (Vector3.Distance(GlobalToLocal(GetHandPos()), GetDesiredPostion()) < 0.05)
            {
                grab = false;
            }
        }
    }


    public void ToggleGrabSRNOT()
    {
        grab = !grab;
    }


    public void ToggleGripSRNOT()
    {
        if (fixedJoint == null) return;
        if (fixedJoint.angularXMotion == ConfigurableJointMotion.Locked)
        {
            fixedJoint.angularXMotion = ConfigurableJointMotion.Free;
            fixedJoint.angularYMotion = ConfigurableJointMotion.Free;
            fixedJoint.angularZMotion = ConfigurableJointMotion.Free;
        }
        else
        {
            Rigidbody other = fixedJoint.connectedBody;
            Destroy(fixedJoint);
            fixedJoint = hand.AddComponent<ConfigurableJoint>();
            fixedJoint.anchor = handTransform.localPosition;
            fixedJoint.xMotion = ConfigurableJointMotion.Locked;
            fixedJoint.yMotion = ConfigurableJointMotion.Locked;
            fixedJoint.zMotion = ConfigurableJointMotion.Locked;
            fixedJoint.angularXMotion = ConfigurableJointMotion.Locked;
            fixedJoint.angularYMotion = ConfigurableJointMotion.Locked;
            fixedJoint.angularZMotion = ConfigurableJointMotion.Locked;
            fixedJoint.projectionMode = JointProjectionMode.PositionAndRotation;
            fixedJoint.connectedBody = other;

        }
    }

    public bool IsHolding()
    {
        return grab;
    }

    public bool IsFixed()
    {
        if (fixedJoint != null && fixedJoint.angularXMotion == ConfigurableJointMotion.Locked)
            return true;
        return false;
    }

    public GameObject GetHeldObject()
    {
        if (fixedJoint == null)
        {
            return fixedJoint.connectedBody.gameObject;
        }
        return null;
    }
}
