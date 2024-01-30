using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class DisplayTasks : MonoBehaviour
{
    private readonly Dictionary<Task, GameObject> texts = new();
    [SerializeField] private GameObject uiTextPerfab;

    void Update()
    {
        foreach (var task in TaskMannager.Singleton.tasks)
        {
            GameObject textUI;
            if (
                Vector3.Angle(task.transform.position - NetworkManager.Singleton.LocalClient.PlayerObject.transform.Find("Body").transform.position,
                NetworkManager.Singleton.LocalClient.PlayerObject.transform.Find("Body").transform.up) > 90
            )
            {
                if (texts.TryGetValue(task, out textUI))
                {
                    Destroy(textUI);
                    texts.Remove(task);
                }
                continue;
            };
            var distance = Vector3.Distance(NetworkManager.Singleton.LocalClient.PlayerObject.transform.Find("Body").transform.position, task.transform.position);
            if (!texts.TryGetValue(task, out textUI))
            {
                textUI = Instantiate(uiTextPerfab, transform);
                texts.Add(task, textUI);
            }
            if (textUI.TryGetComponent(out TextMeshProUGUI textMeshPro))
            {
                textMeshPro.text = "Task: " + ((int)distance).ToString() + "m";
            }
            textUI.transform.position = NetworkManager.Singleton.LocalClient.PlayerObject.GetComponentInChildren<Camera>().WorldToScreenPoint(task.transform.position);

        }
    }
}
