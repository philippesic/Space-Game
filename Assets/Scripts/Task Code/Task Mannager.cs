using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class TaskMannager : MonoBehaviour
{
    public static TaskMannager Singleton { get; private set; }

    private List<Task> tasks = new();

    private void Awake()
    {
        Singleton = this;
    }

    public void AddTask(Task task)
    {
        tasks.Add(task);
    }

    void Update()
    {
        foreach (Task task in tasks)
        {
            if (task.CheckIfDone())
            {
                tasks.Remove(task);
                print("done");
                break;
            }
        }
    }
}
