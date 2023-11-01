using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeCheck : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ledge"))
        {
            player.GrabLedge(other.GetComponent<Ledge>());
            Ledge ledge = other.GetComponent<Ledge>();
            player.transform.position = ledge.hangPosition.transform.position;
        }
    }
}
