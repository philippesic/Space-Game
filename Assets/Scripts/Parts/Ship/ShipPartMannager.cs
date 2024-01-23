using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartMannager : MonoBehaviour
{
    public List<ShipPart> shipParts = new();
    public static ShipPartMannager Singleton;

    private void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }

    public void PrintAllConnectors()
    {
        foreach (var shipPart in shipParts)
        {
            foreach (var connector in shipPart.connectors)
            {
                print(connector.ToString());
            }
        }
    }
}
