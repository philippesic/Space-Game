using Unity.Mathematics;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

public class ArmMovement : NetworkBehaviour
{
    [SerializeField] private HandController leftHand;
    [SerializeField] private HandController rightHand;
    [SerializeField] private float movementSpeed;
    [SerializeField] private float scrollSpeed;
    [SerializeField] private Camera cam;
    [SerializeReference] private PlayerCam playerCamScript;
    private float leftHoldTime = 0;
    private float rightHoldTime = 0;
    [SerializeField] private float neededHoldTime = 0.2f;
    void Update()
    {
        if (!IsOwner) return;

        if (Input.GetKey(KeyCode.Mouse0))
            leftHoldTime += Time.deltaTime + GetMouseData().magnitude * 4;
        if (Input.GetKey(KeyCode.Mouse1))
            rightHoldTime += Time.deltaTime + GetMouseData().magnitude * 4;

        bool isHolding = false;
        if (Input.GetKey(KeyCode.Mouse0) && leftHoldTime > neededHoldTime)
        {
            DoArmMovementHold(leftHand);
            isHolding = true;
        }
        if (Input.GetKey(KeyCode.Mouse1) && rightHoldTime > neededHoldTime)
        {
            DoArmMovementHold(rightHand);
            isHolding = true;
        }
        if (!isHolding)
        {
            if (leftHoldTime <= neededHoldTime && Input.GetKeyUp(KeyCode.Mouse0))
                DoArmMovementClick(leftHand);
            if (rightHoldTime <= neededHoldTime && Input.GetKeyUp(KeyCode.Mouse1))
                DoArmMovementClick(rightHand);
            playerCamScript.CursorLocked = false;
        }
        else
        {
            playerCamScript.CursorLocked = true;
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
            leftHoldTime = 0;
        if (Input.GetKeyUp(KeyCode.Mouse1))
            rightHoldTime = 0;
    }

    private Vector3 GetMouseData()
    {
        return new(
            Input.GetAxis("Mouse X") * 0.003f * movementSpeed,
            Input.GetAxis("Mouse Y") * 0.003f * movementSpeed,
            Input.GetAxis("Mouse ScrollWheel") * 0.1f * scrollSpeed +
            (Input.GetKey(KeyCode.UpArrow) ? 0.2f * Time.deltaTime * movementSpeed : 0) +
            (Input.GetKey(KeyCode.DownArrow) ? -0.2f * Time.deltaTime * movementSpeed : 0)
        );
    }

    private void DoArmMovementHold(HandController handController)
    {
        // move hand
        handController.ShiftPostionServerRpc(GetMouseData());

        // grab with hand
        if (Input.GetKeyDown(KeyCode.Space))
            handController.ToggleGrabServerRpc();

        if (Input.GetKeyDown(KeyCode.LeftShift))
            handController.ToggleGripServerRpc();
    }

    private void DoArmMovementClick(HandController handController)
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        Vector3 newPos;
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            newPos = handController.GlobalToLocal(hit.point);
        }
        else newPos = handController.GlobalToLocal(ray.GetPoint(4));
        handController.SetPostionServerRpc(newPos);
        handController.ToggleGrabServerRpc();
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
