using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plier : Tool
{


    protected override void Use()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.1f);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Wire wire))
            {
                wire.GetPliered();
            }
        }
    }

}