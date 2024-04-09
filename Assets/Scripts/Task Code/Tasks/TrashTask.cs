using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TrashTask : Task
{
    protected override void Awake()
    {
        base.Awake();
        difficulty = 1;
        length = 1;
        type = "Trash";
        money = 5;
    }

    public override bool CheckIfDone()
    {
        if (!gameObject.activeSelf)
        {
            return true;
        }
        return false;
    }
}
