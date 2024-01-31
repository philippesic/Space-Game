using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ShipPart : Part
{
    [System.Serializable]
    public class ShipPartConnection
    {
        [System.Serializable]
        public enum ConnectionType
        {
            square = 1,
            circle = 2,
            frame = 3
        }

        public ShipPartConnection(float size, ConnectionType type, Vector3 direction)
        {
            this.size = size;
            this.type = type;
            this.direction = direction;
        }

        public float size;
        public ConnectionType type;
        public Vector3 direction;

        public override string ToString()
        {
            return size.ToString() + type.ToString() + direction.ToString();
        }
    }

    public List<ShipPartConnection> connectors = new();

    public List<Transform> GetSmallPartConnections()
    {
        Transform partPositions = transform.Find("PartPositions");
        if (partPositions == null) return new();
        List<Transform> transforms = new();
        for (int i = 0; i < partPositions.childCount; i++)
        {
            Transform pos = partPositions.GetChild(i);
            if (pos.CompareTag("Small"))
            {
                transforms.Add(pos);
            }
        }
        return transforms;
    }

    public List<Transform> GetLargePartConnections()
    {
        Transform partPositions = transform.Find("PartPositions");
        if (partPositions == null) return new();
        List<Transform> transforms = new();
        for (int i = 0; i < partPositions.childCount; i++)
        {
            Transform pos = partPositions.GetChild(i);
            if (pos.CompareTag("Large"))
            {
                transforms.Add(pos);
            }
        }
        return transforms;
    }
}
