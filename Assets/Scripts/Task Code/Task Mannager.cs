using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class TaskMannager : MonoBehaviour
{
    public static TaskMannager Singleton { get; private set; }

    private readonly List<Task> pTasks = new();
    public List<Task> tasks = new();
    public float length = 0;
    public float difficulty = 0;


    private void Awake()
    {
        Singleton = this;
    }

    public void MakeTasks()
    {
        MakeTasks(length, difficulty);
    }

    public void AddTask(Task task)
    {
        tasks.Add(task);
    }

    public void AddPTask(Task pTask)
    {
        pTasks.Add(pTask);
    }

    public void MakeTasks(float length, float difficulty)
    {
        if (pTasks.Count == 0)
        {
            return;
        };
        while (length > 0 && pTasks.Count > 0)
        {
            int i = Random.Range(0, pTasks.Count);
            Task task = pTasks[i];
            if (task.length <= length && task.difficulty <= difficulty)
            {
                length -= task.length;
                AddTask(task);
            }
            pTasks.RemoveAt(i);
        }
    }

    void Update()
    {
        List<Task> toRemove = new();
        foreach (Task task in tasks)
        {
            if (task.CheckIfDone())
                toRemove.Add(task);
        }
        foreach (Task task in toRemove)
        {
            tasks.Remove(task);
            GlobalData.Singleton.money += task.money;
        }
    }
}
