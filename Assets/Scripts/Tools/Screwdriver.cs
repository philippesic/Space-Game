using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screwdriver : Tool
{
    protected override void Use()
    {
        print("used");
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, 0.1f))
        {
            print("screw range");
            if (hit.collider.gameObject.TryGetComponent(out Interactable interactable))
                interactable.GetScrewed();
        }
        else
        {
            print("hit nothing");
        }
    }
}
