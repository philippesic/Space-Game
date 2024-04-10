using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.Netcode;
using UnityEngine;

public class BuyTools : MonoBehaviour
{
    [SerializeField] private GameObject tool;
    [SerializeField] private Collider trigger;
    [SerializeField] private TMPro.TextMeshPro costText;
    [SerializeField] private Material unpressed;
    [SerializeField] private Material pressed;
    private int cost;
    private int count = 0;

    void Start()
    {
        cost = tool.GetComponent<Tool>().GetCost();
        costText.text = cost.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            count++;
            gameObject.GetComponent<MeshRenderer>().material = pressed;
            if (GlobalData.Singleton.money >= cost)
            {
                GlobalData.Singleton.money -= cost;
                Instantiate(tool, transform.position + transform.up * -1, transform.rotation, AllPartContainer.Singleton.transform).GetComponent<NetworkObject>().Spawn();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            count--;
            if (count == 0)
            {
                gameObject.GetComponent<MeshRenderer>().material = unpressed;
            }
        }
    }
}
