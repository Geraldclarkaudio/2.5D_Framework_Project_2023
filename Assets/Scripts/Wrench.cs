using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wrench : MonoBehaviour, ICollectable
{
    public int pointValue;
    public float rotSpeed;
    public float bounce;

    public float originalX;
    public float originalY;
    public float originalZ;
    public void Collect(int amount)
    { 
        //update UI with point value.

        Destroy(this.gameObject);
    }

    private void Start()
    {
        originalX = transform.position.x;
        originalY = transform.position.y;   
        originalZ = transform.position.z;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Collect(pointValue);
        }
    }

    private void Update()
    {
        float y = originalY + Mathf.Sin(Time.time * bounce);
        float x =originalX;
        float z = originalZ;

        transform.position = new Vector3(x, y, z);
        transform.Rotate(new Vector3 (0,1,0) * rotSpeed * Time.deltaTime);
    }
}
