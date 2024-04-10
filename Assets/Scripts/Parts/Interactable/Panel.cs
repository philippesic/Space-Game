using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Panel : Interactable
{
    public float health = 5;

    public override void GetLasered(Vector3 pos, float damage = 1)
    {
        health -= damage;
        if (health <= 0)
            Destroy();
    }
}
