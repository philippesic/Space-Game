using System;
using System.Collections;
using System.Collections.Generic;
<<<<<<< Updated upstream
=======
using System.Linq;
using Unity.Netcode;
using Unity.VisualScripting;
>>>>>>> Stashed changes
using UnityEngine;
using Random = UnityEngine.Random;

public class TaskMannager : MonoBehaviour
{
    public static TaskMannager Singleton { get; private set; }

    private readonly List<Task> pTasks = new();
    private readonly List<Task> tasks = new();
    public float lenght = 0;
    public float difficulty = 0;


    private void Awake()
    {
        Singleton = this;
    }

    public void MakeTasks()
    {
        MakeTasks(lenght, difficulty);
    }

    public void AddTask(Task task)
    {
        tasks.Add(task);
        print("made task");
        print(task.gameObject.name);
    }

    public void AddPTask(Task pTask)
    {
        pTasks.Add(pTask);
    }

    void Update()
    {
        if (tasks.Count > 0)
        {
            if (tasks.First().GetComponent<FindTask>() == null)
            {
                tasks.First().gameObject.AddComponent<FindTask>();
            }
        }
        else
        {
            AddTask(pTasks.First());
        }

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
        if (pTasks.Count == 0)
        {
            print("made no tasks");
            return;
        };
        while (lenght > 0 && pTasks.Count > 0)
        {
            int i = Random.Range(0, pTasks.Count);
            print(i);
            Task task = pTasks[i];
            if (task.lenght < lenght && task.difficulty < difficulty)
            {
                //difficulty -= task.difficulty;
                lenght -= task.lenght;
                AddTask(task);
            }
            pTasks.RemoveAt(i);
        }
    }
}
