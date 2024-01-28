using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WireCutTask : Task
{
    public override float difficulty { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public override float lenght { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    public override bool CheckIfDone()
    {
        if (!gameObject.activeSelf)
            return true;
        return false;
    }
}
