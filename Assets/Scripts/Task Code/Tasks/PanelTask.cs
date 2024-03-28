using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PanelTask : Task
{
    [SerializeField] Panel panel;

    protected override void Awake()
    {
        base.Awake();
        difficulty = 1;
        length = 1;
        type = "Panel";
        otherText = "";
        UpdateData();
    }

    // protected override void UpdateData()
    // {
    //     otherText = panel.health.ToString();
    // }

    public override bool CheckIfDone()
    {
        UpdateData();
        if (!gameObject.activeSelf)
            return true;
        return false;
    }
}
