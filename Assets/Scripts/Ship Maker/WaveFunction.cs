using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class WaveFunction : ScriptableObject
{
    public struct PieceData
    {
        public quaternion rotation;
        public ShipPart shipPart;

        public PieceData(quaternion rotation, ShipPart shipPart) : this()
        {
            this.rotation = rotation;
            this.shipPart = shipPart;
        }
    }

    private List<PieceData> pieceDatas;
    private List<PieceData>[,,] array3d;
    private readonly List<Vector3> toUpdate = new();

    private readonly Vector3[] directions = new Vector3[]
    {
        Vector3.forward,
        Vector3.back,
        Vector3.up,
        Vector3.down,
        Vector3.left,
        Vector3.right,
    };

    private readonly quaternion[] rotations = new quaternion[]
    {
        Quaternion.Euler(0, 0, 0),
        Quaternion.Euler(90, 0, 0),
        Quaternion.Euler(180, 0, 0),
        Quaternion.Euler(270, 0, 0),
        Quaternion.Euler(0, 90, 0),
        Quaternion.Euler(0, 270, 0),

        Quaternion.Euler(0, 0, 90),
        Quaternion.Euler(90, 0, 90),
        Quaternion.Euler(180, 0, 90),
        Quaternion.Euler(270, 0, 90),
        Quaternion.Euler(0, 90, 90),
        Quaternion.Euler(0, 270, 90),

        Quaternion.Euler(0, 0, 180),
        Quaternion.Euler(90, 0, 180),
        Quaternion.Euler(180, 0, 180),
        Quaternion.Euler(270, 0, 180),
        Quaternion.Euler(0, 90, 180),
        Quaternion.Euler(0, 270, 180),

        Quaternion.Euler(0, 0, 270),
        Quaternion.Euler(90, 0, 270),
        Quaternion.Euler(180, 0, 270),
        Quaternion.Euler(270, 0, 270),
        Quaternion.Euler(0, 90, 270),
        Quaternion.Euler(0, 270, 270)
    };

    public List<PieceData> GetPos(Vector3 pos)
    {
        return array3d[(int)pos.x, (int)pos.y, (int)pos.z];
    }

    public void SetPos(Vector3 pos, List<PieceData> pieceData)
    {
        array3d[(int)pos.x, (int)pos.y, (int)pos.z] = pieceData;
    }

    public void DoWaveFunction()
    {
        array3d = new List<PieceData>[60, 60, 60];

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

        pieceDatas = new();
        foreach (ShipPart shipPart in ShipPartMannager.Singleton.shipParts)
        {
            foreach (quaternion rotation in rotations)
            {
                pieceDatas.Add(new PieceData(rotation, shipPart));
            }
        }

        while (toUpdate.Count > 0)
        {
            UpdatePos(toUpdate[0]);
            toUpdate.RemoveAt(0);
        }
    }

    public void PlaceLine(Vector3 pos1, Vector3 pos2)
    {
        Vector3 normal = (pos2 - pos1).normalized;
        for (int i = 0; i <= (int)(pos1 - pos2).magnitude; i++)
        {
            SetPos(pos1 + normal * i, pieceDatas.ToList());
            toUpdate.Add(pos1 + normal * i);
        }
    }

    private bool UpdatePos(Vector3 pos)
    {
        foreach (PieceData pieceData in GetPos(pos))
        {
            TryPart(pieceData, pos);
        }

        return false;
    }

    private bool TryPart(PieceData pieceData, Vector3 pos)
    {
        
        return false;
    }
}
