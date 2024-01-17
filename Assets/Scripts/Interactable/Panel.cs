using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Panel : Interactable
{
    [SerializeField] private float health;

    public override void GetLasered(float damage = 1)
    {
        health-= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
