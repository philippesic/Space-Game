using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ArmMovement : NetworkBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    private HandController movingHand;

    private void Awake() {
        movingHand = leftHand.GetComponent<HandController>();
    }

    void Update()
    {
        // sellect hand
        if (Input.GetKey(KeyCode.A))
            movingHand = leftHand.GetComponent<HandController>();
        if (Input.GetKey(KeyCode.D))
            movingHand = rightHand.GetComponent<HandController>();

        // move hand
        var newHandPos = movingHand.GetPostion() + new Vector3(
            Input.GetAxis("Mouse X"),
            0,
            Input.GetAxis("Mouse Y")
            ) / 5;
        movingHand.SetPostion(newHandPos);

        // grab with hand
        if (Input.GetKeyDown(KeyCode.Space))
            movingHand.GetComponent<HandController>().ToggleGrab();
    }   
}
