using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WireCutTask : Task
{
    protected override void Awake() {
        base.Awake();
        difficulty = 1;
        lenght = 4;
    }

    public override bool CheckIfDone()
    {
        if (!gameObject.activeSelf)
            return true;
        return false;
    }
}
