using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : NetworkBehaviour
{
    public bool inSpawn = false;

    void Update()
    {
        if (inSpawn && TaskMannager.Singleton.tasks.Count == 0)
        {
            SceneDataController.Singleton.Leave();
        }
    }
}
