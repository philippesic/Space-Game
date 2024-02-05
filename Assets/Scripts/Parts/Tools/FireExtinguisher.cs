using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : Tool
{
    [SerializeField] Transform output;
    [SerializeField] float sprayForce = 10;
    private Rigidbody rb;
    [SerializeField] private GameObject particles;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        particles.SetActive(false);
    }

    protected override void Use()
    {
        rb.AddForceAtPosition(-output.right * sprayForce, output.position, ForceMode.Force);
        particles.SetActive(true);
    }
    protected override void StopUse()
    {
        particles.SetActive(false);
    }
}
