using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using System.Linq;
using Unity.XR.CoreUtils;

public class DisplayTasks : MonoBehaviour
{
    private readonly Dictionary<Task, GameObject> texts = new();
    [SerializeField] private GameObject uiTextPerfab;
    private Transform playerTransform;
    [SerializeField] private TextMeshProUGUI taskList;
    // [SerializeField] private Camera mainCamera;
    [SerializeField] private Transform taskTexts;
    [SerializeField] private GameObject body;
    // [SerializeField] private bool isList = false;

    void Update()
    {
        // if (NetworkManager.Singleton.IsClient)
        // {
        List<Task> toDelTasks = texts.Keys.ToList();
        string taskListText = "";
        foreach (var task in TaskMannager.Singleton.tasks)
        {
            taskListText += task.GetText() + "\n";
            // if (!isList)
            // {
            GameObject textUI;
            var distance = Vector3.Distance(body.transform.position, task.transform.position);
            if (!texts.TryGetValue(task, out textUI))
            {
                textUI = Instantiate(uiTextPerfab, taskTexts);
                texts.Add(task, textUI);
            }
            else
            {
                toDelTasks.Remove(task);
            }
            if (textUI.GetNamedChild("Text").TryGetComponent(out TextMeshProUGUI textMeshPro))
            {
                string text = task.GetText() + " " + ((int)distance).ToString() + "m";
                if (textMeshPro.text != text)
                {
                    textMeshPro.text = text;
                }
            }
            textUI.transform.position = task.transform.position; //mainCamera.WorldToScreenPoint(task.transform.position);
                                                                 //}
        }
        // }
        foreach (Task task in toDelTasks)
        {
            if (texts.TryGetValue(task, out GameObject textUI))
            {
                Destroy(textUI);
                texts.Remove(task);
            }
        }
        if (taskList.text != taskListText) //isList && 
        {
            taskList.text = taskListText;
        }
        // }
    }
}
