using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Components;
using UnityEngine;

[RequireComponent(typeof(NetworkRigidbody))]
[RequireComponent(typeof(NetworkObject))]
public class Part : NetworkBehaviour
{
    void Start()
    {
        SetupPart();
    }

    private void SetupPart()
    {
        if (TryGetComponent(out Rigidbody rigidbody))
        {
            rigidbody.useGravity = false;
            if (rigidbody.drag == 0f && rigidbody.angularDrag == 0.05f) {
                rigidbody.angularDrag = 0;
            }
        }
    }

    public void Destroy()
    {
        GetComponent<NetworkObject>().Despawn(false);
        gameObject.SetActive(false);
    }
}
