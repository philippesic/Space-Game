using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public static class SmallerPartAdder
{
    public static void AddSmallerParts(ShipPartContainer container)
    {
        List<Transform> connectionPoints = container.GetSmallPartConnections();
        foreach (Transform connectionPoint in connectionPoints)
        {
            if (Random.Range(0, 4) == 0)
            {
                List<GameObject> smallerparts = ShipPartMannager.Singleton.smallParts;
                container.AddPart(smallerparts[Random.Range(0, smallerparts.Count)], connectionPoint.position, connectionPoint.rotation);
            }
        }
    }
}
