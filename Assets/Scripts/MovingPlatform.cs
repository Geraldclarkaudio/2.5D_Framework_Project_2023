using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{

    [SerializeField]
    private Transform waypoint1;
    [SerializeField]
    private Transform waypoint2;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private bool _switching;

    void FixedUpdate()
    {
        if (_switching == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint2.position, _speed * Time.deltaTime);
        }
        else if (_switching == false)
        {
            transform.position = Vector3.MoveTowards(transform.position, waypoint1.position, _speed * Time.deltaTime);

        }
        if ((transform.position == waypoint1.position))
        {
            //move down
            _switching = true;
        }

        else if ((transform.position == waypoint2.position))
        {
            //move up
            _switching = false;
        }
    }
}
