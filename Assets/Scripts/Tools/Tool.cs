using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    [SerializeField] private bool isToggle;
    private GameObject grabberGameObject;
    private bool isOn;
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
        if (grabberGameObject != null && grabberGameObject.TryGetComponent(out Player player) && player.IsOwner)
        {
            if (isToggle)
            {
                if (Input.GetKeyDown(KeyCode.F))
                    isOn = !isOn;
            }
            else
            {
                if (Input.GetKey(KeyCode.F))
                    isOn = true;
            }
        }
        if (isOn)
        {
            Use();
            if (!isToggle)
            {
                isOn = false;
            }
        }
    }

    protected abstract void Use();
}
