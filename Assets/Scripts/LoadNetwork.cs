using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNetwork : MonoBehaviour
{
    void Update()
    {
        if (NetworkManager.Singleton != null)
            SceneManager.LoadScene("Joining Game Scene");
    }
}
