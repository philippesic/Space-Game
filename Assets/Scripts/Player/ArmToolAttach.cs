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
                Debug.Log("tool check return tool");
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
            if (collider.gameObject.TryGetComponent(out Tool tool))
            {
                print(tool.IsGrabbed());
                if (!tool.IsGrabbed())
                {
                    closestGameObject = collider.gameObject;
                    return true;
                }
            }
        }
        closestGameObject = null;
        return false;
    }
}
