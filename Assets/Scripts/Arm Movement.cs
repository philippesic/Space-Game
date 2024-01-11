using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ArmMovement : NetworkBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    private GameObject movingHand;

    private void Awake() {
        movingHand = leftHand;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.A))
            movingHand = leftHand;
        if (Input.GetKey(KeyCode.D))
            movingHand = rightHand;
    
        var newHandPos = movingHand.GetComponent<ConfigurableJoint>().targetPosition + new Vector3(
            Input.GetAxis("Mouse X"),
            0,
            Input.GetAxis("Mouse Y")
            ) / 5;
        if (newHandPos.magnitude > 1)
            newHandPos = newHandPos.normalized * 1f;
        movingHand.GetComponent<ConfigurableJoint>().targetPosition = newHandPos;
    }   
}
