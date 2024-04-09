using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireExtinguisher : Tool
{
    [SerializeField] Transform output;
    [SerializeField] float sprayForce = 10;
    private Rigidbody rb;
    [SerializeField] private GameObject[] particles = new GameObject[2];
    protected override void SetUpOtherStuff()
    {
        rb = GetComponent<Rigidbody>();
        foreach (GameObject p in particles) { p.SetActive(false); }
    }

    protected override void Use()
    {
        rb.AddForceAtPosition(-output.right * sprayForce, output.position, ForceMode.Force);
        foreach (GameObject p in particles) { p.SetActive(true); }
    }
    protected override void StopUse()
    {
        foreach (GameObject p in particles) { p.SetActive(false); }
    }
}
