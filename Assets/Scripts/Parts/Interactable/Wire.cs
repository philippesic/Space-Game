using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Wire : Interactable
{

    public override void GetPliered(Vector3 pos)
    {
        Destroy();
    }
}
