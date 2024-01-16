using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Panel : Interactable
{
    [SerializeField] private int health;

    public override void GetLasered(int damage = 1)
    {
        health-= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
