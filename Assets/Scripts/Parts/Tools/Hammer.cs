using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Tool
{
    private void OnCollisionEnter(Collision other)
    {
        if (!IsServer) return;
        float force = other.impulse.magnitude;
        if (force > 1 && (other.collider.TryGetComponent(out Interactable interactable) || other.collider.transform.parent.TryGetComponent(out interactable)))
        {
            interactable.GetHammered(force / 40);
        }
    }
}

