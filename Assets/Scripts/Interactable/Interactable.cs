using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Interactable : NetworkBehaviour
{
    public virtual void GetCut() {}
    public virtual void GetLasered() {}
    public virtual void GetHammered() {}
    public virtual void GetScrewed() {}
}
