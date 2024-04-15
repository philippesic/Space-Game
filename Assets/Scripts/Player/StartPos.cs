using System.Collections;
using UnityEngine;
public class StartPos : MonoBehaviour
{
    int movedPlayer = 0;
    [SerializeField] Vector3 playerPos = new();
    void Update()
    {
        GameObject player = GameObject.Find("Body");
        if (player != null)
        {
            if (movedPlayer == 50)
                player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            movedPlayer++;
        }

    }
}