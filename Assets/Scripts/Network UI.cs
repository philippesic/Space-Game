using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button host;
    [SerializeField] private Button client;

    private void Awake() {
        host.onClick.AddListener(() => {
            //NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = IPManager.GetLocalIPAddress();
            NetworkManager.Singleton.StartHost();
            Destroy(gameObject);
        });
        client.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
            Destroy(gameObject);
        });
    }
}
