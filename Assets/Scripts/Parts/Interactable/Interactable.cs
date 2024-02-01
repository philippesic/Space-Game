using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Interactable : Part
{
    protected float health = 5;
    public virtual void GetPliered() { }
    public virtual void GetLasered(float damage = 1) { }
    public virtual void GetHammered(float damage = 1) { }
    public virtual void GetScrewed() { }
}
