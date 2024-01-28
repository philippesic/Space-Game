using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEditor.Callbacks;
using UnityEngine;
using Random = UnityEngine.Random;

public static class WaveFunction
{
    public struct PieceData
    {
        public Quaternion rotation;
        public ShipPart shipPart;

        public PieceData(quaternion rotation, ShipPart shipPart) : this()
        {
            this.rotation = rotation;
            this.shipPart = shipPart;
        }
    }

    private static int areaXSize;
    private static int areaYSize;
    private static int areaZSize;


    private static List<PieceData> pieceDatas;
    private static List<PieceData>[,,] array3d;
    private static readonly List<Vector3> toUpdate = new();

    private static readonly Vector3[] directions = new Vector3[]
    {
        Vector3.forward,
        Vector3.back,
        Vector3.up,
        Vector3.down,
        Vector3.left,
        Vector3.right,
    };

    private static readonly quaternion[] rotations = new quaternion[]
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

    public static List<PieceData> GetPos(Vector3 pos)
    {
        return GetPos((int)pos.x, (int)pos.y, (int)pos.z);
    }

    public static void SetPos(Vector3 pos, List<PieceData> pieceData)
    {
        SetPos((int)pos.x, (int)pos.y, (int)pos.z, pieceData);
    }

    public static List<PieceData> GetPos(int x, int y, int z)
    {
        if (0 <= x && 0 <= y && 0 <= z && array3d.GetLength(0) > x && array3d.GetLength(1) > y && array3d.GetLength(2) > z)
            return array3d[x, y, z];
        return new();
    }

    public static void SetPos(int x, int y, int z, List<PieceData> pieceData)
    {
        if (0 <= x && 0 <= y && 0 <= z && array3d.GetLength(0) > x && array3d.GetLength(1) > y && array3d.GetLength(2) > z)
            array3d[x, y, z] = pieceData;
    }

    private static void SendToContainer(ShipPartContainer container)
    {
        for (int x = 0; x < areaXSize; x++)
        {
            for (int y = 0; y < areaYSize; y++)
            {
                for (int z = 0; z < areaZSize; z++)
                {
                    List<PieceData> posData = GetPos(x, y, z);
                    if (posData.Count > 0)
                    {
                        container.AddPart(posData[0].shipPart.gameObject, new Vector3(x, y, z), posData[0].rotation, true);
                    }
                }
            }
        }
    }

    public static void Setup(Vector3 size)
    {
        areaXSize = (int)size.x;
        areaYSize = (int)size.y;
        areaZSize = (int)size.z;
        array3d = new List<PieceData>[areaXSize, areaYSize, areaZSize];

        for (int x = 0; x < areaXSize; x++)
        {
            for (int y = 0; y < areaYSize; y++)
            {
                for (int z = 0; z < areaZSize; z++)
                {
                    SetPos(x, y, z, new());
                }
            }
        }

        pieceDatas = new();
        foreach (ShipPart shipPart in ShipPartMannager.Singleton.shipParts)
        {
            foreach (Quaternion rotation in rotations)
            {
                pieceDatas.Add(new PieceData(rotation, shipPart));
            }
        }
    }

    public static void DoWaveFunction(ShipPartContainer container)
    {
        bool keepGoing = true;
        while (keepGoing)
        {
            while (toUpdate.Count > 0)
            {
                if (UpdatePos(toUpdate[0]))
                {
                    foreach (Vector3 direction in directions)
                    {
                        toUpdate.Add(toUpdate[0] + direction);
                    }
                }
                toUpdate.RemoveAt(0);
            }

            keepGoing = CheckIfDone();
        }
        SendToContainer(container);
    }

    private static bool CheckIfDone()
    {
        for (int x = 0; x < areaXSize; x++)
        {
            for (int y = 0; y < areaYSize; y++)
            {
                for (int z = 0; z < areaZSize; z++)
                {
                    var datas = GetPos(x, y, z);
                    if (datas.Count > 1)
                    {
                        SetPos(new(x, y, z), new List<PieceData>() { datas[Random.Range(0, datas.Count - 1)] });
                        foreach (Vector3 direction in directions)
                        {
                            toUpdate.Add(new Vector3(x, y, z) + direction);
                        }
                        toUpdate.Add(new(x, y, z));
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public static void PlaceLine(Vector3 pos1, Vector3 pos2)
    {
        Vector3 normal = (pos2 - pos1).normalized;
        for (int i = 0; i <= (int)(pos1 - pos2).magnitude; i++)
        {
            if (GetPos(pos1 + normal * i).Count == 0)
            {
                SetPos(pos1 + normal * i, pieceDatas.ToList());
                toUpdate.Add(pos1 + normal * i);
            }
        }
    }

    private static bool UpdatePos(Vector3 pos)
    {
        bool doOtherUpdates = false;
        List<PieceData> dataAtPos = GetPos(pos);
        int i = 0;
        while (i < dataAtPos.Count)
        {
            PieceData pieceData = dataAtPos[i];
            if (!TryPart(pieceData, pos))
            {
                doOtherUpdates = true;
                dataAtPos.Remove(pieceData);
            }
            else
            {
                i++;
            }
        }
        return doOtherUpdates;
    }

    private static bool TryPart(PieceData pieceData, Vector3 pos)
    {
        foreach (Vector3 direction in directions)
        {
            if (!TryDirection(pieceData, pos, direction))
                return false;
        }
        return true;
    }

    private static bool TryDirection(PieceData pieceData, Vector3 pos, Vector3 direction)
    {
        ShipPart.ShipPartConnection partConnection = null;
        foreach (ShipPart.ShipPartConnection connection in pieceData.shipPart.connectors)
        {
            if (CheckIfSameDirection(pieceData.rotation * connection.direction, direction))
            {
                partConnection = connection;
                break;
            }
        }
        List<PieceData> pieceDatas = GetPos(pos + direction);
        if (partConnection == null)
        {
            foreach (PieceData data in pieceDatas)
            {
                foreach (ShipPart.ShipPartConnection connection in data.shipPart.connectors)
                {
                    if (CheckIfSameDirection(data.rotation * connection.direction, -direction))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        foreach (PieceData data in pieceDatas)
        {
            foreach (ShipPart.ShipPartConnection connection in data.shipPart.connectors)
            {
                if (CheckIfSameDirection(data.rotation * connection.direction, -direction))
                {
                    if (connection.type == partConnection.type && connection.size == partConnection.size)
                    {
                        return true;
                    }
                    break;
                }
            }
        }
        return false;
    }

    private static bool CheckIfSameDirection(Vector3 v1, Vector3 v2)
    {
        return (v1 - v2).magnitude < 0.01;
    }
}
