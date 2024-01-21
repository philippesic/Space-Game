using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Tool : Part
{
    [SerializeField] private bool isToggle;
    private GameObject grabberGameObject;
    private bool isOn = false;

    public void Grabbed(GameObject grabber)
    {
        if (grabberGameObject == null)
            grabberGameObject = grabber;
        if (grabberGameObject.TryGetComponent(out Player player))
        {
            if (player.OwnerClientId != OwnerClientId)
                GetComponent<NetworkObject>().ChangeOwnership(player.OwnerClientId);
        }
    }

    public void Dropped(GameObject grabber)
    {
        if (grabberGameObject == grabber)
            if (grabberGameObject.TryGetComponent(out Player player))
            {
                if (NetworkManager.ServerClientId != OwnerClientId)
                    GetComponent<NetworkObject>().ChangeOwnership(NetworkManager.ServerClientId);
            }
        grabberGameObject = null;
    }

    public void Update()
    {
        if (IsOwner && (grabberGameObject != null || !IsServer))
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                PressedServerRpc(true);
            }
            else if (Input.GetKey(KeyCode.F))
            {
                PressedServerRpc(false);
            }
        }
        if (IsServer && isOn)
        {
            Use();
            if (!isToggle)
            {
                isOn = false;
            }
        }
    }

    [ServerRpc]
    private void PressedServerRpc(bool isKeyDown)
    {
        if (grabberGameObject != null && grabberGameObject.TryGetComponent(out Player player))
        {
            if (isToggle)
            {
                if (isKeyDown)
                    isOn = !isOn;
            }
            else
            {
                isOn = true;
            }
        }
    }
    protected virtual void Use() {}
}
