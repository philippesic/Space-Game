using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Player : MonoBehaviour
{
    public bool inSpawn = false;
    public int isHoldingStaticCount = 0;
    public bool isHoldingStatic = false;
    [SerializeField] Rigidbody bodyRB;
    int i = 0;

    void Update()
    {
        isHoldingStatic = isHoldingStaticCount > 0;
        if (isHoldingStatic)
        {
            bodyRB.drag = 8;
        }
        else if (inSpawn)
        {
            bodyRB.drag = 2;
        }
        else
        {
            bodyRB.drag = 0;
        }
        if (i < 10)
        {
            i++;
        }
        else if (inSpawn && TaskMannager.Singleton.tasks.Count == 0)
        {
            SceneDataController.Singleton.Leave();
        }
    }
}
