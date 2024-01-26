using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] private ShipPartContainer container = null;

    void Start()
    {
        print("-run-");
        if (container != null)
        {
            MakeSpaceStation.MakeStation(container, new Vector3(7, 4, 20));
        }
    }
}
