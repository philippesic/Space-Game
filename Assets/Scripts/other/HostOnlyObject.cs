using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class HostOnlyObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // if (NetworkManager.Singleton.IsClient && !NetworkManager.Singleton.IsServer)
        // {
        //     Destroy(gameObject);
        // }
    }
}
