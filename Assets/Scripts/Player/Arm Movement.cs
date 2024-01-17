using Unity.Netcode;
using UnityEngine;

public class ArmMovement : NetworkBehaviour
{
    [SerializeField] private HandController leftHand;
    [SerializeField] private HandController rightHand;
    [SerializeField] private float movementSpeed;

    void Update()
    {
        if (!IsOwner) return;

        // sellect hand
        if (Input.GetKey(KeyCode.Mouse0))
            DoArmMovement(leftHand);
        if (Input.GetKey(KeyCode.Mouse1))
            DoArmMovement(rightHand);
    }

    private void DoArmMovement(HandController handController)
    {
        // move hand
        handController.ShiftPostionServerRpc(
            movementSpeed * new Vector3(
                Input.GetAxis("Mouse X") * 0.002f,
                Input.GetAxis("Mouse Y") * 0.002f,
                Input.GetAxis("Mouse ScrollWheel") * 0.1f + (Input.GetKey(KeyCode.UpArrow) ? 0.2f * Time.deltaTime : 0) + (Input.GetKey(KeyCode.DownArrow) ? -0.2f * Time.deltaTime : 0)
            )
        );

        // grab with hand
        if (Input.GetKeyDown(KeyCode.Space))
            handController.ToggleGrabServerRpc();
    }
}
