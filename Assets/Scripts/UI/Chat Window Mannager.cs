using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChatWindowMannager : MonoBehaviour
{
    [SerializeField] private ScrollRect scrollRect;
    [SerializeField] private TextMeshProUGUI textMeshPro;
    private readonly List<string> textList = new();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
            scrollRect.gameObject.SetActive(!scrollRect.gameObject.activeSelf);
    }

    public void Add(string text)
    {
        textList.Add(text);
        UpdateText();
    }

    private void UpdateText()
    {
        textMeshPro.text = "";
        foreach (string text in textList)
        {
            textMeshPro.text += text + "\n";
        }
    }

    void Start()
    {
        Application.logMessageReceived += HandleLog;
    }

    void HandleLog(string logString, string stackTrace, LogType type)
    {
        Add(logString);
    }
}
