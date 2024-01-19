using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class Interactable : NetworkBehaviour
{
    public virtual void GetPliered() { }
    public virtual void GetLasered(float damage = 1) { }
    public virtual void GetHammered(float damage = 1) { }
    public virtual void GetScrewed() { }
    public void Destroy()
    {
        GetComponent<NetworkObject>().Despawn(true);
    }
}
