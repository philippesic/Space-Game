using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class ArmMovement : NetworkBehaviour
{
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    private HandController movingHand;

    private void Start()
    {
        movingHand = leftHand.GetComponent<HandController>();
    }

    void Update()
    {
        if (IsOwner)
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
                var newHandPos = movingHand.GetDesiredPostion() + 10 * Time.deltaTime * new Vector3(
                    Input.GetAxis("Mouse X"),
                    0,
                    Input.GetAxis("Mouse Y")
                    );
                //newHandPos.z = movingHand.GetComponent<HandController>().desiredZ;
                movingHand.SetPostion(newHandPos);

                // grab with hand
                if (Input.GetKeyDown(KeyCode.Space))
                    movingHand.GetComponent<HandController>().ToggleGrab();
            }
        }
    }
}
