using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class Tool : Part
{
    [SerializeField] private bool isToggle;
    [SerializeField] protected int cost;
    private bool isOn = false;
    private bool isHeld = false;
    [SerializeField] private List<InputActionReference> use = new();
    [SerializeField] private bool isPressed = false;

    public override void Grabbed(GameObject grabber)
    {
        isHeld = true;
        if (grabberGameObjects.Count == 0)
        {
            if (grabber.TryGetComponent(out Player player))
            {
                // if (player.OwnerClientId != OwnerClientId)
                //     GetComponent<NetworkObject>().ChangeOwnership(player.OwnerClientId);
            }
        }

        grabberGameObjects.Add(grabber);
    }

    public void GiveInput(InputActionReference inputAction)
    {
        use.Add(inputAction);
    }

    public void RemoveInput(InputActionReference inputAction)
    {
        use.Remove(inputAction);
    }

    public override void Dropped(GameObject grabber)
    {
        isHeld = false;
        if (grabberGameObjects.Contains(grabber))
        {
            // if (grabber.TryGetComponent(out Player player))
            // {
            //     if (NetworkManager.ServerClientId != OwnerClientId)
            //         GetComponent<NetworkObject>().ChangeOwnership(NetworkManager.ServerClientId);
            // }
            grabberGameObjects.Remove(grabber);
        }
    }

    public bool HeldByLocalClient()
    {
        foreach (GameObject grabber in grabberGameObjects)
        {
            if (grabber.TryGetComponent(out Player player))
            {
                // if (player.OwnerClientId == NetworkManager.Singleton.LocalClientId)
                return true;
            }
        }
        return false;
    }

    private bool ButtonPressed()
    {
        foreach (var action in use)
        {
            if (action.action.IsPressed())
            {
                return true;
            }
        }
        return false;
    }

    public void Update()
    {
        if (HeldByLocalClient())
        {
            if (use != null)
            {
                if (ButtonPressed() && !isPressed)
                {
                    PressedSRNOT(true);
                }
                else if (isPressed)
                {
                    PressedSRNOT(false);
                }
                isPressed = ButtonPressed();
            }
        }
        // if (IsServer)
        // {
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
        // }
    }


    public void PressedSRNOT(bool isKeyDown)
    {
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
    public bool IsHeld()
    {
        return isHeld;
    }
    public int GetCost()
    {
        return cost;
    }

    protected virtual void Use() { }
    protected virtual void StopUse() { }
}
