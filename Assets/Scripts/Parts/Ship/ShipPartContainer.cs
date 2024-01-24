using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartContainer : MonoBehaviour
{
    private List<ShipPart> parts = new();

    public void AddPart(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        print(prefab.name);
        GameObject partGameObject = Instantiate(prefab, position, rotation);
        partGameObject.transform.SetParent(transform);
        if (TryGetComponent(out ShipPart part))
        {
            parts.Add(part);
        }
    }

    void Start()
    {
        WaveFunction.DoWaveFunction(this);
    }
}
