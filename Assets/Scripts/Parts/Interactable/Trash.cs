using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Trash : Interactable
{
    public override void GetHammered(float damage = 1)
    {
        health -= damage;
        if (health <= 0)
        {
            if (TryGetComponent(out ConfigurableJoint configurableJoint))
            {
                Destroy(configurableJoint);
            }
        }
    }
}
