using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Panel : Interactable
{
    [SerializeField] private float health = 1;

    public override void GetLasered(float damage = 1)
    {
        health-= damage;
        if (health <= 0)
            Destroy();
    }
}
