using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomEyeMovement : MonoBehaviour
{
    Vector3 pos = new();
    float t = 0f;
    void Update()
    {
        t += Time.deltaTime;
        if (t > 0.5 && (UnityEngine.Random.value < Time.deltaTime || t > 1.5))
        {
            t = 0;
            pos = Quaternion.AngleAxis(UnityEngine.Random.value * 360, Vector3.up) * Vector3.forward * 0.776f * UnityEngine.Random.value;
        }
        transform.localPosition += (pos - transform.localPosition) * Math.Min(Time.deltaTime * 10, 0.5f);
    }
}
