using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : Tool
{
    protected override void Use()
    {
        Ray ray = new(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.collider.gameObject.TryGetComponent(out Interactable interactable))
            {
                if (hit.collider.gameObject.CompareTag("Crack"))
                {
                    interactable.GetLasered(hit.point, hit.collider, Time.deltaTime);
                }
                else
                {
                    interactable.GetLasered(hit.point, Time.deltaTime);
                }
            }
            else if (hit.collider.gameObject.transform.parent.TryGetComponent(out interactable))
            {
                if (hit.collider.gameObject.CompareTag("Crack"))
                {
                    interactable.GetLasered(hit.point, hit.collider, Time.deltaTime);
                }
                else
                {
                    interactable.GetLasered(hit.point, Time.deltaTime);
                }
            }
        }
    }
}
