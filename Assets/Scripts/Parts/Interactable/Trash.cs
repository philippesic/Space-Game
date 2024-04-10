using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Trash : Interactable
{
    public float health = 5;

    public override void GetHammered(float damage = 1)
    {
        health -= damage;
        print(health);
        if (health <= 0)
        {
            if (TryGetComponent(out Rigidbody rigidbody))
            {
            rigidbody.constraints = RigidbodyConstraints.None;
            }
        }
    }
}
