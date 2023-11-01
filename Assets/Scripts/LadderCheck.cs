using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LadderCheck : MonoBehaviour
{
    Player player;

    private void Start()
    {
        player = GetComponentInParent<Player>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ladder"))
        {
            player.GrabLadder(other.GetComponent<Ladder>());
            Ladder ladder = other.GetComponent<Ladder>();
            player.transform.position = ladder.grabPoint.transform.position;
        }

        if (other.CompareTag("LadderEnd"))
        {
            player.GetOffLadder();
        }
    }
}
