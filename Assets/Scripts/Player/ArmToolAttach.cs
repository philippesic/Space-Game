using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ArmToolAttach : MonoBehaviour
{
    [SerializeField] private Transform grabPoint;
    private FixedJoint fixedJoint;
    private GameObject attachedObject;

    void Start()
    {
    }

    void Update()
    {
        if (attachedObject == null)
        {
            if (GetTool(out GameObject closestGameObject))
            {
                attachedObject = closestGameObject;
                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.connectedBody = closestGameObject.GetComponent<Rigidbody>();
            }
        }
        else
        {
            if (attachedObject.TryGetComponent(out Part part))
            {
                if (part.IsGrabbed())
                {
                    Destroy(fixedJoint);
                    attachedObject = null;
                }
            }
        }
    }

    public bool GetTool(out GameObject closestGameObject)
    {
        Collider[] colliders = Physics.OverlapSphere(grabPoint.position, 0.2f);
        closestGameObject = null;
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.TryGetComponent(out Tool tool) || collider.transform.parent.TryGetComponent(out tool))
            {
                if (!tool.IsGrabbed())
                {
                    closestGameObject = tool.gameObject;
                    return true;
                }
            }
        }
        closestGameObject = null;
        return false;
    }
}
