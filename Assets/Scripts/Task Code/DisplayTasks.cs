using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class DisplayTasks : MonoBehaviour
{
    private readonly Dictionary<Task, GameObject> texts = new();
    [SerializeField] private GameObject uiTextPerfab;
    private Transform playerTransform;

    void Update()
    {
        if (playerTransform == null)
        {
            if (NetworkManager.Singleton.IsClient)
            {
                playerTransform = NetworkManager.Singleton.LocalClient.PlayerObject.transform;
            }
        }
        else
        {
            foreach (var task in TaskMannager.Singleton.tasks)
            {
                GameObject textUI;
                if (Vector3.Angle(task.transform.position - playerTransform.Find("Body").transform.position, playerTransform.Find("Body").transform.up) > 90)
                {
                    if (texts.TryGetValue(task, out textUI))
                    {
                        Destroy(textUI);
                        texts.Remove(task);
                    }
                    continue;
                };
                var distance = Vector3.Distance(playerTransform.Find("Body").transform.position, task.transform.position);
                if (!texts.TryGetValue(task, out textUI))
                {
                    textUI = Instantiate(uiTextPerfab, transform);
                    texts.Add(task, textUI);
                }
                if (textUI.TryGetComponent(out TextMeshProUGUI textMeshPro))
                {
                    string text = "Task: " + ((int)distance).ToString() + "m";
                    if (textMeshPro.text != text)
                    {
                        textMeshPro.text = text;
                    }
                }
                textUI.transform.position = playerTransform.GetComponentInChildren<Camera>().WorldToScreenPoint(task.transform.position);

            }
        }
    }
}
