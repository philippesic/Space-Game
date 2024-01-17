using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Tool
{
    protected override void Use()
    {
        print("used");
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 0.1f))
        {
            print("hit");
            if (hit.collider.gameObject.TryGetComponent(out Interactable interactable))
                interactable.GetHammered(1);
        }
        else
        {
            print("hit nothing");
        }
    }
}
