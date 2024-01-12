using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ArmMovement : NetworkBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    private HandController movingHand;

    private void Awake()
    {
        movingHand = leftHand.GetComponent<HandController>();
    }

    void Update()
    {
        // sellect hand
        if (Input.GetKey(KeyCode.Mouse0))
            movingHand = leftHand.GetComponent<HandController>();
        else if (Input.GetKey(KeyCode.Mouse1))
            movingHand = rightHand.GetComponent<HandController>();
        else
            movingHand = null;
        if (movingHand != null)
        {
            // move hand
            var newHandPos = movingHand.GetDesiredPostion() + 4 * Time.deltaTime * new Vector3(
                Input.GetAxis("Mouse X"),
                Input.GetAxis("Mouse Y"),
                0
                );
            newHandPos.z = movingHand.GetComponent<HandController>().desiredZ;
            movingHand.SetPostion(newHandPos);

            // grab with hand
            if (Input.GetKeyDown(KeyCode.Space))
                movingHand.GetComponent<HandController>().ToggleGrab();
        }
    }
}
