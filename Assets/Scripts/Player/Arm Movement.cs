using Unity.Netcode;
using UnityEngine;

public class ArmMovement : NetworkBehaviour
{
    [SerializeField] private HandController leftHand;
    [SerializeField] private HandController rightHand;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float scrollSpeed;


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
            new Vector3(
                Input.GetAxis("Mouse X") * 0.003f * movementSpeed,
                Input.GetAxis("Mouse Y") * 0.003f * movementSpeed,
                Input.GetAxis("Mouse ScrollWheel") * 0.1f * scrollSpeed +
                (Input.GetKey(KeyCode.UpArrow) ? 0.2f * Time.deltaTime * movementSpeed : 0) +
                (Input.GetKey(KeyCode.DownArrow) ? -0.2f * Time.deltaTime * movementSpeed : 0)
            )
        );

        // grab with hand
        if (Input.GetKeyDown(KeyCode.Space))
            handController.ToggleGrabServerRpc();
        
        if (Input.GetKeyDown(KeyCode.LeftShift))
            handController.ToggleGripServerRpc();
    }

    public HandController GetLeft()
    {
        return leftHand;
    }

    public HandController GetRight()
    {
        return rightHand;
    }
}
