using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SceneManagement;
using UnityEngine;

public class HandController : MonoBehaviour
{
    private ConfigurableJoint joint;
    private FixedJoint fixedJoint;
    [SerializeField] private float maxHandMovement = 1;
    private Vector3 desiredPos = new();
    [SerializeField] GameObject display;

    private void Awake()
    {
        joint = GetComponent<ConfigurableJoint>();
    }

    public void SetPostion(Vector3 pos)
    {
        if (pos.magnitude > maxHandMovement)
            desiredPos = pos.normalized * maxHandMovement;
        else
            desiredPos = pos;
        UpdateJoint();
    }

    private void UpdateJoint()
    {
        joint.targetPosition = desiredPos;
    }

    public Vector3 GetPostion()
    {
        return joint.targetPosition;
    }

    public void ToggleGrab()
    {
        if (fixedJoint == null || fixedJoint.IsDestroyed())
        {
            Debug.Log("grabbing");
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1);
            Collider closestCollider = null;
            float closestColliderDistance = 100;
            foreach (Collider collider in colliders)
            {
                if (collider.gameObject == gameObject) continue;
                float dis = (collider.ClosestPoint(transform.position) - transform.position).magnitude;
                if (dis < closestColliderDistance)
                {
                    closestCollider = collider;
                    closestColliderDistance = dis;
                }
            }
            if (closestCollider != null)
            {
                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.breakForce = 20;
                fixedJoint.connectedBody = closestCollider.gameObject.GetComponent<Rigidbody>();
            }
        }
        else
        {
            Destroy(fixedJoint);
        }
    }
}
