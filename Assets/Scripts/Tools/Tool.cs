using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Tool : MonoBehaviour
{
    public GameObject tool;
    public int id;
    public bool isToggle;
    public bool isHeld;
    public bool inUse;

    public abstract void Use();

    public void Update()
    {
        if (isHeld)
        {
            if (isToggle)
            {
                if (Input.GetKeyDown(KeyCode.F))
                    Use();
            }
            else
            {
                if (Input.GetKey(KeyCode.F))
                    Use();
            }
        }
    }

}
