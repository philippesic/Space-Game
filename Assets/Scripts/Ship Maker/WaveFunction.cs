using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveFunction : ScriptableObject
{
    private List<ShipPart>[,,] array3d;

    public void DoWaveFunction()
    {
        array3d = new List<ShipPart>[60, 60, 60];

        for (int x = 0; x < 60; x++)
        {
            for (int y = 0; y < 60; y++)
            {
                for (int z = 0; z < 60; z++)
                {
                    array3d[x, y, z] = new();
                }
            }
        }
    }

    public void PlaceLine(Vector3 pos1, Vector3 pos2)
    {
        Vector3 normal = (pos2 - pos1).normalized;
        for (int i = 0; i <= (int) (pos1 - pos2).magnitude; i++)
        {
            Vector3 pos = pos1 + normal * i;
            array3d[(int) pos.x, (int) pos.y, (int) pos.z] = ShipPartMannager.Singleton.shipParts.ToList();
        }
    }
}
