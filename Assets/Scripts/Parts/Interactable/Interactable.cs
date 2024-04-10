using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Interactable : Part
{
    public virtual void GetPliered(Vector3 pos) { }
    public virtual void GetLasered(Vector3 pos, float damage = 1) { }
    public virtual void GetLasered(Vector3 pos, Collider collider, float damage = 1) { }
    public virtual void GetHammered(Vector3 pos, float damage = 1) { }
    public virtual void GetScrewed(Vector3 pos) { }
}
