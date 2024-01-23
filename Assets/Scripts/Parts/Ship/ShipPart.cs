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
            circle = 2
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

    public List<ShipPartConnection> connectors = new() ;
}
