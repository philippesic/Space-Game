using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDataController : MonoBehaviour
{
    public static SceneDataController Singleton;

    void Start()
    {
        Singleton = this;
        NetworkManager.Singleton.GetComponent<UnityTransport>().ConnectionData.Address = GameLoadingData.ip;
        if (GameLoadingData.hosting)
            NetworkManager.Singleton.StartHost();
        else
            NetworkManager.Singleton.StartClient();
    }

    public void Leave()
    {
        NetworkManager.Singleton.Shutdown();
        SceneManager.LoadScene("Joining Game Scene");
    }
}
