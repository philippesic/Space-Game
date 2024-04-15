using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class stopCameraMovement : MonoBehaviour
{
    [SerializeField] Transform camPos;

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = -camPos.localPosition;
    }
}
