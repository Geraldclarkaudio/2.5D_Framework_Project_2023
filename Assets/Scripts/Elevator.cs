using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField]
    private Transform topPosition;
    [SerializeField]
    private Transform bottomPosition;
    [SerializeField]
    private Transform targetPosition;

    [SerializeField]
    private float _speed;
    [SerializeField]
    private bool switching = false;
    [SerializeField]
    public bool _activated = false;
    [SerializeField]
    private bool _reachedTarget;

    void FixedUpdate()
    {
        if (_activated == true)
        {
            if (_reachedTarget == false)
            {
                if (switching == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, bottomPosition.position, _speed * Time.deltaTime);
                }
                else if (switching == false)
                {
                    transform.position = Vector3.MoveTowards(transform.position, topPosition.position, _speed * Time.deltaTime);

                }
            }

            //if at the top
            if ((transform.position == topPosition.position))
            {
                //move down
                switching = true;
                _reachedTarget = true;
                StartCoroutine(WaitAtPoint());
            }

            else if ((transform.position == bottomPosition.position))
            {
                //move up
                switching = false;
                _reachedTarget = true;
                StartCoroutine(WaitAtPoint());

            }
        }       
    }

    IEnumerator WaitAtPoint()
    {
        yield return new WaitForSeconds(5.0f);
        _reachedTarget = false;
        //startt moving;
    }
}
