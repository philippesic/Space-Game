using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class DisplayTasks : MonoBehaviour
{
    private readonly Dictionary<Task, TextMeshProUGUI> texts = new();

    void Update()
    {
        print("displaying tasks");
        print(TaskMannager.Singleton.tasks.Count);
        foreach (var task in TaskMannager.Singleton.tasks)
        {
            var distance = Vector3.Distance(NetworkManager.Singleton.LocalClient.PlayerObject.transform.position, task.transform.position);
            if (!texts.TryGetValue(task, out TextMeshProUGUI textUI))
            {
                texts.Add(task, this.AddComponent<TextMeshProUGUI>());
            }
            textUI.text = "Distance: " + distance.ToString() + "m";
            // distanceText.GetComponent<TextMeshProUGUI>().text = "Distance: " + distance.ToString() + "m";

            // direction = player.gameObject.transform.position - gameObject.transform.position;
            // direction.z = 0f;

            // arrow.transform.up = direction.normalized;
        }
    }
}
