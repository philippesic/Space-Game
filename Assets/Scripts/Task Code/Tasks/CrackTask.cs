using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrackTask : Task
{
    [SerializeField] Crack crack;

    protected override void Awake()
    {
        base.Awake();
        difficulty = 1;
        length = 1;
        type = "Crack";
        otherText = "";
        UpdateData();
        money = 5;
    }


    public override bool CheckIfDone()
    {
        UpdateData();
        if (!gameObject.activeSelf)
        {
            return true;
        }
        return false;
    }
}
