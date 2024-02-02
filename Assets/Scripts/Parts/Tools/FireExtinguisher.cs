using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : Tool
{
    [SerializeField] Transform output;
    [SerializeField] float sprayForce = 10;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    protected override void Use()
    {
        rb.AddForceAtPosition(-output.right * sprayForce, output.position, ForceMode.Force);
    }
}
