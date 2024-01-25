using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public static class MakeSpaceStation
{
    public static void MakeStation(ShipPartContainer container, Vector3 size)
    {
        Vector3 padding = new(1, 1, 1);
        WaveFunction.Setup(size + padding * 2);

        // Make shape

        Vector2 center = size / 2 + padding;
        // Main
        WaveFunction.PlaceLine(new Vector3(center.x, center.y, padding.z), new Vector3(center.x, center.y, size.z + padding.z));
        // Other
        for (int i = 0; i < math.ceil(size.z / 2); i++)
        {

            switch (Random.Range(0, 10))
            {
                case 0:
                    {
                        WaveFunction.PlaceLine(
                            new Vector3(center.x, padding.y, (size.z / math.ceil(size.z / 2) * i) + padding.z + (size.z / math.ceil(size.z / 8) / 2)),
                            new Vector3(center.x, size.y + padding.y, (size.z / math.ceil(size.z / 2) * i) + padding.z + (size.z / math.ceil(size.z / 8) / 2))
                        );
                        break;
                    }
                case 1:
                    {
                        WaveFunction.PlaceLine(
                            new Vector3(padding.x, center.y, (size.z / math.ceil(size.z / 2) * i) + padding.z + (size.z / math.ceil(size.z / 8) / 2)),
                            new Vector3(size.x + padding.x, center.y, (size.z / math.ceil(size.z / 2) * i) + padding.z + (size.z / math.ceil(size.z / 8) / 2))
                        );
                        break;
                    }
                case 2:
                    {
                        WaveFunction.PlaceLine(
                            new Vector3(center.x, padding.y, (size.z / math.ceil(size.z / 2) * i) + padding.z + (size.z / math.ceil(size.z / 8) / 2)),
                            new Vector3(center.x, size.y + padding.y, (size.z / math.ceil(size.z / 2) * i) + padding.z + (size.z / math.ceil(size.z / 8) / 2))
                        );
                        break;
                    }
                case 3:
                    {
                        WaveFunction.PlaceLine(
                            new Vector3(padding.x, center.y, (size.z / math.ceil(size.z / 2) * i) + padding.z + (size.z / math.ceil(size.z / 8) / 2)),
                            new Vector3(size.x + padding.x, center.y, (size.z / math.ceil(size.z / 2) * i) + padding.z + (size.z / math.ceil(size.z / 8) / 2))
                        );
                        break;
                    }
                default:
                break;
            }

        }
        WaveFunction.DoWaveFunction(container);


        SmallerPartAdder.AddSmallerParts(container);
    }
}
