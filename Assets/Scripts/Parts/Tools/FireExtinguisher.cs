using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : Tool
{
    Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected override void Use()
    {
        rb.AddForce(-transform.forward * 10f);
    }
}
