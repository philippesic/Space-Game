using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class BuyTools : MonoBehaviour
{
    [SerializeField] private GameObject tool;
    [SerializeField] private int cost;
    [SerializeField] private Collider trigger;
    [SerializeField] private TMPro.TextMeshPro costText;

    void Start()
    {
        costText.text = cost.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (GlobalData.Singleton.money >= cost)
            {
                GlobalData.Singleton.money -= cost;
                Instantiate(tool, transform.position + transform.up * -1, transform.rotation, AllPartContainer.Singleton.transform).GetComponent<NetworkObject>().Spawn();
            }
        }
    }
}
