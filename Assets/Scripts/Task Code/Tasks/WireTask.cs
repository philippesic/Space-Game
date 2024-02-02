using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WireTask : Task
{
    protected override void Awake()
    {
        base.Awake();
        difficulty = 1;
        length = 1;
        type = "Wire";
    }

    public override bool CheckIfDone()
    {
        if (!gameObject.activeSelf)
            return true;
        return false;
    }
}
