using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;

public class SceneDataLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = GameLoadingData.ip;
        if (GameLoadingData.hosting)
            NetworkManager.Singleton.StartHost();
        else
            NetworkManager.Singleton.StartClient();
    }
}
