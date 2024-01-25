using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartContainer : MonoBehaviour
{
    private readonly List<ShipPart> shipParts = new();
    private readonly List<GameObject> otherObjects = new();

    [SerializeField] private float shipPartScale = 2;
    [SerializeField] private float otherScale = 1;

    
    public void AddPart(GameObject prefab, Vector3 position, Quaternion rotation)
    {
        GameObject partGameObject = Instantiate(prefab, position, rotation, transform);
        if (partGameObject.TryGetComponent(out ShipPart shipPart))
        {
            partGameObject.transform.position = position * shipPartScale;
            partGameObject.transform.localScale = new(shipPartScale, shipPartScale, shipPartScale); 
            shipParts.Add(shipPart);
        } else
        {
            partGameObject.transform.localScale = new(otherScale, otherScale, otherScale); 
            otherObjects.Add(partGameObject);
        }
    }

    public List<Transform> GetSmallPartConnections()
    {
        List<Transform> transforms = new();
        foreach (ShipPart shipPart in shipParts)
        {
            transforms.AddRange(shipPart.GetSmallPartConnections());
        }
        return transforms;
    }

    public List<Transform> GetLargePartConnections()
    {
        List<Transform> transforms = new();
        foreach (ShipPart shipPart in shipParts)
        {
            transforms.AddRange(shipPart.GetLargePartConnections());
        }
        return transforms;
    }
}
