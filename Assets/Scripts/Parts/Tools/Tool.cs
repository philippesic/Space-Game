using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Tool : Part
{
    [SerializeField] private bool isToggle;
    private bool isOn = false;

    public override void Grabbed(GameObject grabber)
    {
        if (grabberGameObjects.Count == 0)
        {
            if (grabber.TryGetComponent(out Player player))
            {
                if (player.OwnerClientId != OwnerClientId)
                    GetComponent<NetworkObject>().ChangeOwnership(player.OwnerClientId);
            }
        }
        grabberGameObjects.Add(grabber);
    }

    public override void Dropped(GameObject grabber)
    {
        if (grabberGameObjects.Contains(grabber))
        {
            if (grabber.TryGetComponent(out Player player))
            {
                if (NetworkManager.ServerClientId != OwnerClientId)
                    GetComponent<NetworkObject>().ChangeOwnership(NetworkManager.ServerClientId);
            }
            grabberGameObjects.Remove(grabber);
        }
    }

    public bool HeldByLocalClient()
    {
        foreach (GameObject grabber in grabberGameObjects)
        {
            if (grabber.TryGetComponent(out Player player))
            {
                if (player.OwnerClientId == NetworkManager.Singleton.LocalClientId)
                    return true;
            }
        }
        return false;
    }

    public void Update()
    {
        print(HeldByLocalClient());
        if (HeldByLocalClient() && !IsServer)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                print("here");
                PressedServerRpc(true);
            }
            else if (Input.GetKey(KeyCode.F))
            {
                PressedServerRpc(false);
            }
        }
        if (IsServer)
        {
            if (isOn)
            {
                Use();
                if (!isToggle)
                {
                    isOn = false;
                }
            }
            else
                StopUse();
        }
    }

    [Rpc(SendTo.Server)]
    public void PressedServerRpc(bool isKeyDown)
    {
        print(isKeyDown);
        foreach (GameObject gameObject in grabberGameObjects)
        {
            if (gameObject != null && gameObject.TryGetComponent(out Player player))
            {
                if (isToggle)
                {
                    if (isKeyDown)
                        isOn = !isOn;
                    if (!isOn)
                    {
                        StopUse();
                    }
                }
                else
                {
                    isOn = true;
                }
            }
        }
    }

    protected virtual void Use() { }
    protected virtual void StopUse() { }
}
