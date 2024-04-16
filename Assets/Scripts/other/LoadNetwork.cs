using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNetwork : MonoBehaviour
{
    int i = 0;
    void Update()
    {
        if (i > 10)
            SceneManager.LoadScene("Joining Game Scene");
        i ++;
    }
}
