using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Crack : Interactable
{
    public float health = 5;
    [SerializeField] protected Dictionary<Collider, float> positions = new Dictionary<Collider, float>();

    public override void GetLasered(Vector3 pos, Collider collider, float damage = 1)
    {

        if (positions.ContainsKey(collider))
        {
            positions[collider] -= damage;
        }
        if (positions[collider] <= 0)
        {
            positions.Remove(collider);
            Destroy(collider.gameObject);
        }

        if (positions.Count <= 0)
            Destroy();
    }
}
