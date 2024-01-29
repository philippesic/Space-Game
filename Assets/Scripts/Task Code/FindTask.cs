using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using TMPro;

public class FindTask : MonoBehaviour
{
    [SerializeField] private GameObject distanceText;
    [SerializeField] private GameObject arrow;
    // private List<Player> players = new List<Player>();
    private float distance;
    private Vector3 direction;
    private Player player;

    void Start()
    {
        foreach (NetworkObject obj in FindObjectsOfType<NetworkObject>())
        {
            if (obj.GetComponent<Player>() != null)
                //players.Add(obj.GetComponent<Player>());
                player = obj.GetComponent<Player>();
        }
        distanceText = GameObject.Find("Distance");
        arrow = GameObject.Find("Arrow");
    }

    void FixedUpdate()
    {
        distance = Vector3.Distance(player.gameObject.transform.position, gameObject.transform.position);
        distanceText.GetComponent<TextMeshProUGUI>().text = "Distance: " + distance.ToString() + "m";

        direction = player.gameObject.transform.position - gameObject.transform.position;
        direction.z = 0f;

        arrow.transform.up = direction.normalized;
    }
}
