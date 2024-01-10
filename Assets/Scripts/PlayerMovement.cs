using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class PlayerMovement : NetworkBehaviour
{
    void Update()
    {
        if (!IsOwner) return;
        
        Vector3 movement = new();
        if (Input.GetKey(KeyCode.W))
        {
            movement.y += 1;
        }
        if (Input.GetKey(KeyCode.S))
        {
            movement.y -= 1;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movement.x += 1;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movement.x -= 1;
        }
        transform.position = transform.position + movement * 0.1f;
    }
}
