using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointAtCamera : MonoBehaviour
{
    void Update()
    {
        GameObject camObj = GameObject.Find("Main Camera");
        if (camObj != null && camObj.TryGetComponent(out Camera cam))
        {
            transform.rotation = Quaternion.LookRotation(transform.position - cam.transform.position, cam.transform.rotation * Vector3.up);
        }
    }
}
