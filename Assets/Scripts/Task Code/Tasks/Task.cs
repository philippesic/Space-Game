using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public abstract float difficulty {get; set;}
    public abstract float lenght {get; set;}

    void Start()
    {
        TaskMannager.Singleton.AddPTask(this);
    }

    public abstract bool CheckIfDone();
}
