using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    private void Start()
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Part otherPart))
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("static station"))
            {
                Rigidbody rg = other.gameObject.GetComponent<Rigidbody>();
                rg.drag = 1.2f;
                rg.angularDrag = 1;
            }
        }
        else if (other.gameObject.TryGetComponent(out ArmMovement playerBody))
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("static station"))
            {
                Rigidbody rg = other.gameObject.GetComponent<Rigidbody>();
                rg.drag = 2;
                Player player = other.gameObject.transform.parent.GetComponent<Player>();
                player.inSpawn = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Part otherPart))
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("static station"))
            {
                Rigidbody rg = other.gameObject.GetComponent<Rigidbody>();
                rg.drag = 0;
                rg.angularDrag = 0;
            }
        }
        else if (other.gameObject.TryGetComponent(out ArmMovement playerBody))
        {
            if (other.gameObject.layer != LayerMask.NameToLayer("static station"))
            {
                Rigidbody rg = other.gameObject.GetComponent<Rigidbody>();
                rg.drag = 0;
                Player player = other.gameObject.transform.parent.GetComponent<Player>();
                player.inSpawn = false;
            }
        }
    }
}
