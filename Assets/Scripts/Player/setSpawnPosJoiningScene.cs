using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class setSpawnPosJoiningScene : MonoBehaviour
{
    int i = 0;
    [SerializeField] Transform camPos;

    // Update is called once per frame
    void Update()
    {
        if (i < 10)
        {
            i++;
            transform.rotation = Quaternion.AngleAxis(
                -Vector3.Angle(Vector3.ProjectOnPlane(camPos.localRotation * Vector3.forward, Vector3.up), Vector3.forward) *
                Math.Sign((camPos.localRotation * Vector3.forward).x), Vector3.up
            );
            transform.localPosition = transform.rotation * (-camPos.localPosition + new Vector3(0, 1.36144f, 0));
        }
    }
}
