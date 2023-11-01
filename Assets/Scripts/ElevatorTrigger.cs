using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorTrigger : MonoBehaviour
{
    [SerializeField]
    private Elevator elevator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player._canUseElevator = true;
            player.currentElevator = elevator;
            player.transform.parent = transform.parent.transform;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            player._canUseElevator = false;
            player.currentElevator = null;
            player.transform.parent = null;

        }
    }
}
