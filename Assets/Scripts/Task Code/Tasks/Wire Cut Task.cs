using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WireCutTask : Task
{
    public override bool CheckIfDone()
    {
        if (!gameObject.activeSelf)
            return true;
        return false;
    }
}
