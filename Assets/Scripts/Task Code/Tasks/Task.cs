using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    void Start()
    {
        TaskMannager.Singleton.AddTask(this);
    }

    public abstract bool CheckIfDone();
}
