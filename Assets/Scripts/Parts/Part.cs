using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody))]
[RequireComponent(typeof(NetworkObject))]
public class Part : NetworkBehaviour
{
    protected List<GameObject> grabberGameObjects = new();

    public bool IsGrabbed()
    {
        return grabberGameObjects.Count > 0;
    }

    public virtual void Grabbed(GameObject grabber)
    {
        grabberGameObjects.Add(grabber);
    }

    public virtual void Dropped(GameObject grabber)
    {
        grabberGameObjects.Remove(grabber);
    }

    void Start()
    {
        SetupPart();
        SetUpOtherStuff();
    }

    protected virtual void SetUpOtherStuff() { }

    private void SetupPart()
    {
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.isKinematic = false;
            rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
            rigidbody.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
            rigidbody.useGravity = false;
            if (rigidbody.drag == 0f && rigidbody.angularDrag == 0.05f)
            {
                rigidbody.angularDrag = 0;
            }
        }
    }

    public void Destroy()
    {
        GetComponent<NetworkObject>().Despawn(false);
        gameObject.SetActive(false);
    }

    public void SetCollion(bool isOn)
    {
        if (TryGetComponent(out Rigidbody rigidbody))
            if (isOn)
            {
                rigidbody.excludeLayers = 0;
            }
            else
            {
                rigidbody.excludeLayers = -1;
            }
    }
}
