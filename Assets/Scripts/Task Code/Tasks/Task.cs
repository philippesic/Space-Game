using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public float difficulty;
    public float length;
    public string type;

    protected virtual void Awake()
    {
        TaskMannager.Singleton.AddPTask(this);
    }

    public abstract bool CheckIfDone();
}
