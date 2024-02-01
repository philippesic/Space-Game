using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelTask : Task
{
    protected override void Awake()
    {
        base.Awake();
        difficulty = 1;
        length = 1;
    }

    public override bool CheckIfDone()
    {
        if (!gameObject.activeSelf)
            return true;
        return false;
    }
}
