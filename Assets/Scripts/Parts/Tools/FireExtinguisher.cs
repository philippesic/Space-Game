using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : Tool
{
    [SerializeField] Transform output;
    [SerializeField] float sprayForce = 10;
    private Rigidbody rb;
    private ParticleSystem ps;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ps = GetComponentInChildren<ParticleSystem>();
    }
    protected override void Use()
    {
        rb.AddForceAtPosition(-output.right * sprayForce, output.position, ForceMode.Force);
        // if (!ps.isPlaying)
        // {
        //     ps.Play();
        // }
    }
    protected override void StopUse()
    {
        // if (!ps.isStopped)
        // {
        //     ps.Stop();
        // }
    }
}
