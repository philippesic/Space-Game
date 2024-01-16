using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Tool
{
    void Start()
    {
        id = 0;
        isToggle = false;
    }
    public override void Use()
    {

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            hit.collider.gameObject.GetComponent<Interactable>().GetLasered();
        }
        else
        {
            Debug.Log("BAAAAAAAHAAAAHAHAAHAAAAAHAAAAAA STORMTROOPER ASS AIM 🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡🤡");
        }
    }
}
