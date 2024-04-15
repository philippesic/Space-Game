using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using Unity.Mathematics;
using Unity.Netcode;
using UnityEngine;

public class Crack : Interactable
{
    private List<float> healths = new();
    [SerializeField] private List<Vector3> positions = new();

    void Awake()
    {
        foreach (Vector3 pos in positions)
        {
            healths.Add(5);
        }
        // foreach (Transform child in transform)
        // {
        //     Collider collider = child.GetComponent<Collider>();
        //     if (collider != null && collider.isTrigger)
        //     {
        //         positions.Add(collider, health);
        //     }
        // }
    }
    public override void GetLasered(Vector3 hitPos, float damage = 1)
    {
        for (int i = 0; i < positions.Count; i++)
        {
            Vector3 pos = positions[i];
            if (Vector3.Distance(transform.TransformPoint(pos), hitPos) < 0.2f)
            {
                // healths[positions.IndexOf(pos)] -= damage;
                // if (healths[positions.IndexOf(pos)] <= 0)
                // {
                    healths.RemoveAt(positions.IndexOf(pos));
                    positions.Remove(pos);
                // }
            }
            if (positions.Count <= 0)
                Destroy();
        }

        // if (positions.ContainsKey(collider))
        // {
        //     positions[collider] -= damage;
        // }
        // if (positions[collider] <= 0)
        // {
        //     positions.Remove(collider);
        //     Destroy(collider.gameObject);
        // }

        // if (positions.Count <= 0)
        //     Destroy();
    }
}
