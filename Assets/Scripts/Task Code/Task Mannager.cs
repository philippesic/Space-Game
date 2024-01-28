using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class TaskMannager : MonoBehaviour
{
    public static TaskMannager Singleton { get; private set; }

    private readonly List<Task> pTasks = new();
    private readonly List<Task> tasks = new();

    private void Awake()
    {
        Singleton = this;
    }

    public void AddTask(Task task)
    {
        tasks.Add(task);
    }

    public void AddPTask(Task pTask)
    {
        pTasks.Add(pTask);
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

    public void MakeTasks(float lenght, float difficulty)
    {
        
    }
}
