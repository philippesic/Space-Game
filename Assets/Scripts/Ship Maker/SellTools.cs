using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SellTools : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Tool>(out Tool tool) || other.transform.parent.TryGetComponent(out tool))
        {
            if (!tool.IsHeld())
            {
                GlobalData.Singleton.money += (int)Math.Floor((double)tool.GetCost() / 2);
                Destroy(tool.gameObject);
            }
        }
    }
}
