using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class AllPartContainer : MonoBehaviour
{
    public static AllPartContainer Singleton {get; private set; }

    public void AddCreatedPart(GameObject networkObject)
    {
        // networkObject.Spawn();
        // networkObject.TrySetParent(gameObject);
        networkObject.transform.parent = transform;
    }

    private void Awake()
    {
        Singleton = this;
    }
}
