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
                fixedJoint.connectedBody = attachedObject.GetComponent<Rigidbody>();
                attachedObject.GetComponent<Part>().SetCollion(false);
            }
        }
        else
        {
            if (attachedObject.TryGetComponent(out Part part))
            {
                if (part.IsGrabbed())
                {
                    Destroy(fixedJoint);
                    attachedObject.GetComponent<Part>().SetCollion(true);
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
                if (tool.TryGetComponent(out Rigidbody rigidbody) && rigidbody.excludeLayers == 0 && !tool.IsGrabbed())
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
