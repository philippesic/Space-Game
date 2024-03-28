using Unity.VisualScripting;
using UnityEngine;

public class ArmToolAttach : MonoBehaviour
{

    private GameObject arm;
    private GameObject heldTool;
    private ConfigurableJoint fixedJoint;
    private Transform pos;

    void Start()
    {
        arm = this.gameObject;
        pos = arm.GetComponentInChildren<Transform>();
    }

    void Update()
    {
        if (heldTool == null)
        {
            Debug.Log("held tool null");
            if (GetTool(out GameObject closestGameObject))
            {
                Debug.Log("tool check return tool");
                heldTool = closestGameObject;
                fixedJoint = arm.AddComponent<ConfigurableJoint>();
                fixedJoint.anchor = pos.position;
                fixedJoint.xMotion = ConfigurableJointMotion.Locked;
                fixedJoint.yMotion = ConfigurableJointMotion.Locked;
                fixedJoint.zMotion = ConfigurableJointMotion.Locked;
                fixedJoint.projectionMode = JointProjectionMode.PositionAndRotation;
                fixedJoint.angularXMotion = ConfigurableJointMotion.Locked;
                fixedJoint.angularYMotion = ConfigurableJointMotion.Locked;
                fixedJoint.angularZMotion = ConfigurableJointMotion.Locked;
                fixedJoint.connectedBody = closestGameObject.GetComponent<Rigidbody>();
            }
        }

    }

    public bool GetTool(out GameObject closestGameObject)
    {
        Collider[] colliders = Physics.OverlapSphere(pos.position, 0.125f);
        closestGameObject = null;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out Tool tool))
            {
                if (!tool.IsHeld())
                {
                    closestGameObject = collider.gameObject;
                    break;
                }
            }
        }
        return closestGameObject != null;
    }
}
