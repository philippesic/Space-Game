using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class HandController : MonoBehaviour
{
    [SerializeField] private ArticulationBody upper;
    [SerializeField] private ArticulationBody lower;
    [SerializeField] private ArticulationBody hand;
    [SerializeField] GameObject body;
    [SerializeField] private float maxHandMovement = 1;

    private FixedJoint fixedJoint;
    public float desiredZ = 1f;
    private Vector3 desiredPos = new(0, 0, 0);
    
    private bool tryGrab = false;
    
    private void Awake()
    {
        SetPostion(new(0, 0, desiredZ));
    }

    void Update()
    {
        if (tryGrab) TryGrab();
    }

    public void SetPostion(Vector3 pos)
    {
        if (pos.magnitude > maxHandMovement)
            desiredPos = pos.normalized * maxHandMovement;
        else
            desiredPos = pos;
        UpdateJoints();
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
        Vector3 rotations = DoIK(desiredPos, 1f, 1f);
        upper.SetDriveTarget(ArticulationDriveAxis.Y, rotations.x);
        upper.SetDriveTarget(ArticulationDriveAxis.X, rotations.y);
        lower.SetDriveTarget(ArticulationDriveAxis.Z, rotations.z);
    }

    private Vector3 DoIK(Vector3 position, float L1, float L2)
    {
        return new()
        {
            x = (float)(180 / Math.PI * Math.Atan2(position.x, position.z)),
            y = (float)(180 / Math.PI *
            (
                math.acos((Math.Pow(L1, 2) + Math.Pow(position.magnitude, 2) - Math.Pow(L2, 2)) / (2 * L1 * position.magnitude))
                + Math.Atan2(-position.y, Math.Sqrt(Math.Pow(position.x, 2) + Math.Pow(position.z, 2)))
            )),
            z = 180 - (float)(180 / Math.PI * math.acos((Math.Pow(L1, 2) + Math.Pow(L2, 2) - Math.Pow(position.magnitude, 2)) / (2 * L1 * L2)))
        };
    }

    private void TryGrab()
    {
        Debug.Log("grabbing");
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.3f);
        Collider closestCollider = null;
        float closestColliderDistance = 100;
        foreach (Collider collider in colliders)
        {
            if (
                body == collider.gameObject ||
                upper.gameObject == collider.gameObject ||
                lower.gameObject == collider.gameObject ||
                hand.gameObject == collider.gameObject
                ) continue;
            if (collider.gameObject.GetComponent<Rigidbody>() == null) continue;
            float dis = (collider.ClosestPoint(transform.position) - transform.position).magnitude;
            if (dis < closestColliderDistance)
            {
                closestCollider = collider;
                closestColliderDistance = dis;
            }
        }
        // if (closestCollider != null)
        // {
        //     Debug.Log("grabbed");
        //     tryGrab = false;
        //     fixedJoint = gameObject.AddComponent<FixedJoint>();
        //     fixedJoint.connectedBody = closestCollider.gameObject.GetComponent<Rigidbody>();
        // }
        // else
        {
            Vector3 before = desiredPos;
            desiredZ = desiredPos.z + 0.1f * Time.deltaTime;
            SetPostion(before + new Vector3(0, 0, 0.4f * Time.deltaTime));
            if ((before - desiredPos).magnitude < 0.005)
            {
                tryGrab = false;
                var pos = desiredPos;
                pos.z = 1;
                desiredZ = 1;
                SetPostion(pos);
            }
        }
    }

    public void ToggleGrab()
    {
        if (fixedJoint == null || fixedJoint.IsDestroyed())
        {
            tryGrab = true;
        }
        else
        {
            tryGrab = false;
            var pos = desiredPos;
            desiredZ = 1;
            pos.z = 1;
            SetPostion(pos);
            Destroy(fixedJoint);
        }
    }
}
