using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public abstract class Tool : NetworkBehaviour
{
    [SerializeField] private bool isToggle;
    private GameObject grabberGameObject;
    private readonly NetworkVariable<bool> isOn = new(false);
    public void Grabbed(GameObject grabber)
    {
        if (grabberGameObject == null)
            grabberGameObject = grabber;
    }

    public void Dropped(GameObject grabber)
    {
        if (grabberGameObject == grabber)
            grabberGameObject = null;
    }

    public void Update()
    {
        if (IsOwner) {
            if (grabberGameObject != null && grabberGameObject.TryGetComponent(out Player player) && player.IsOwner)
            {
                if (isToggle)
                {
                    if (Input.GetKeyDown(KeyCode.F))
                        isOn.Value = !isOn.Value;
                }
                else
                {
                    if (Input.GetKey(KeyCode.F))
                        isOn.Value = true;
                }
            }
        }
        if (IsServer){
            if (isOn.Value)
            {
                Use();
                if (!isToggle)
                {
                    isOn.Value = false;
                }
            }
        }
    }

    private void Destroy() {
        
    }

    protected abstract void Use();
}
