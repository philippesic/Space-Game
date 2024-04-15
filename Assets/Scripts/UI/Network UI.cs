using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] public string sceneToLoad;
    [SerializeField] private Button host;
    [SerializeField] private Button client;
    [SerializeField] private TextMeshProUGUI ipText;
    [SerializeField] private TMP_InputField ipInput;

    private void Awake()
    {
        ipText.text = IPManager.GetLocalIPAddress();
        host.onClick.AddListener(() =>
        {
            GameLoadingData.ip = IPManager.GetLocalIPAddress();
            GameLoadingData.hosting = true;
            LoadGame();
        });
        client.onClick.AddListener(() =>
        {
            GameLoadingData.ip = ipInput.text;
            GameLoadingData.hosting = false;
            LoadGame();
        });
    }

    private void LoadGame()
    {
        if (GlobalData.Singleton != null)
        {
            GlobalData.Singleton.money += GlobalData.Singleton.startMoney;
        }
        SceneManager.LoadScene(sceneToLoad, LoadSceneMode.Single);
    }
}
