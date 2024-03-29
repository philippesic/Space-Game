using Unity.VisualScripting;
using UnityEngine;

public class ArmToolAttach : MonoBehaviour
{

    private FixedJoint fixedJoint;
    private GameObject attachedObject;

    void Start()
    {
    }

    void Update()
    {
        if (fixedJoint == null || ReferenceEquals(fixedJoint, null)) return;
        if (attachedObject == null)
        {
            if (GetTool(out GameObject closestGameObject))
            {
                Debug.Log("tool check return tool");
                attachedObject = closestGameObject;
                fixedJoint.connectedBody = closestGameObject.GetComponent<Rigidbody>();
            }
        }
        else
        {
            GameObject heldObject = null;
            if (heldObject != null)
            {
                if (ReferenceEquals(heldObject, attachedObject))
                {
                    fixedJoint.connectedBody = GetComponent<Rigidbody>();
                    attachedObject = null;
                }
            }
        }
    }

    public bool GetTool(out GameObject closestGameObject)
    {
        Collider[] colliders = Physics.OverlapSphere(Quaternion.Inverse(transform.rotation) * fixedJoint.anchor + transform.position, 0.2f);
        closestGameObject = null;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out Tool tool))
            {
                if (!tool.IsGrabbed())
                {
                    closestGameObject = collider.gameObject;
                    break;
                }
            }
        }
        return closestGameObject != null;
    }
}
