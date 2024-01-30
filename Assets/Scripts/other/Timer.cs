using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : ScriptableObject
{
    float time = Time.realtimeSinceStartup;

    public float End()
    {
        return Time.realtimeSinceStartup - time;
    }
}
