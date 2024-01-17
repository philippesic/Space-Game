using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class ArmMovement : NetworkBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;

    void Update()
    {
        if (IsOwner)
        {
            // sellect hand
            if (Input.GetKey(KeyCode.Mouse0))
                DoArmMovement(leftHand.GetComponent<HandController>());
            if (Input.GetKey(KeyCode.Mouse1))
                DoArmMovement(rightHand.GetComponent<HandController>());
        }
    }

    private void DoArmMovement(HandController handController)
    {
        // move hand
        var newHandPos = handController.GetDesiredPostion() + 10 * Time.deltaTime * new Vector3(
            Input.GetAxis("Mouse X"),
            Input.GetAxis("Mouse Y"),
            Input.GetAxis("Mouse ScrollWheel") * 15 + (Input.GetKey(KeyCode.UpArrow) ? 0.4f : 0) + (Input.GetKey(KeyCode.DownArrow) ? -0.4f : 0)
            );
        handController.SetPostion(newHandPos);

        // grab with hand
        if (Input.GetKeyDown(KeyCode.Space))
            handController.GetComponent<HandController>().ToggleGrab();
    }
}
