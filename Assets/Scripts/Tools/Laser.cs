using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Tool
{
    protected override void Use()
    {
        print("used");
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            print("hit");
            if (hit.collider.gameObject.TryGetComponent(out Interactable interactable))
                interactable.GetLasered(Time.deltaTime);
        }
        else
        {
            print("hit nothing");
        }
    }
}
