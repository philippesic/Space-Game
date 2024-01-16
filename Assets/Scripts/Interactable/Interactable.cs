using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Interactable : NetworkBehaviour
{
    public virtual void GetCut() {}
    public virtual void GetLasered(int damage = 1) {}
    public virtual void GetHammered(int damage = 1) {}
    public virtual void GetScrewed() {}
}
