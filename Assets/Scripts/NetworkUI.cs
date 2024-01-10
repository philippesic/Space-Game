using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class NetworkUI : MonoBehaviour
{
    [SerializeField] private Button Host;
    [SerializeField] private Button Client;

    private void Awake() {
        Host.onClick.AddListener(() => {
            NetworkManager.Singleton.StartHost();
        });
        Client.onClick.AddListener(() => {
            NetworkManager.Singleton.StartClient();
        });
    }
}
