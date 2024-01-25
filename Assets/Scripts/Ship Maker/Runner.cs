using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Runner : MonoBehaviour
{
    [SerializeField] private ShipPartContainer container = null;

    void Start()
    {
        if (container != null)
        {
            MakeSpaceStation.MakeStation(container, new Vector3(7, 4, 20));
        }
    }

}
