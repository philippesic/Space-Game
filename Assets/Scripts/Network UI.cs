using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button host;
    [SerializeField] private Button client;
    [SerializeField] private TextMeshProUGUI ipText;
    [SerializeField] private TMP_InputField ipInput;

    private void Awake() {
        ipText.text = IPManager.GetLocalIPAddress();
        host.onClick.AddListener(() => {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = IPManager.GetLocalIPAddress();
            NetworkManager.Singleton.StartHost();
            Destroy(gameObject);
        });
        client.onClick.AddListener(() => {
            Destroy(GetComponentInChildren<Camera>().gameObject);
            NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = ipInput.text;
            NetworkManager.Singleton.StartClient();
            Destroy(gameObject);
        });
    }
}
