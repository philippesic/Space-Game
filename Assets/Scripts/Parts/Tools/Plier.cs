using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Plier : Tool
{

    [SerializeField] private Transform pos;
    protected override void Use()
    {
        Collider[] colliders = Physics.OverlapSphere(pos.position, 0.02f);
        foreach (Collider collider in colliders)
        {
            if (collider.TryGetComponent(out Wire wire) || collider.transform.parent.TryGetComponent(out wire))
            {
                wire.GetPliered(collider.ClosestPoint(pos.position));
            }
        }
    }

}