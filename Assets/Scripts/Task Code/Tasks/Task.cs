using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public float difficulty;
    public float length;
    public string type;
    public string otherText = "";
    [SerializeField] protected int money;

    protected virtual void Awake()
    {
        UpdateData();
        TaskMannager.Singleton.AddPTask(this);
    }

    public abstract bool CheckIfDone();

    protected virtual void UpdateData() { }

    public string GetText()
    {
        UpdateData();
        if (otherText == "")
        {
            return type;
        }
        return type + "(" + otherText + ")";
    }
    public void AddMoney(int reward)
    {
        GlobalData.Singleton.money += reward;
    }
}
