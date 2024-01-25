using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPartMannager : MonoBehaviour
{
    public List<ShipPart> shipParts = new();
    public List<GameObject> smallParts = new();
    public List<GameObject> largeParts = new();
    public static ShipPartMannager Singleton;

    private void Awake()
    {
        Singleton = this;
        DontDestroyOnLoad(gameObject);
    }
}
