using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    public GameObject test;
    Rigidbody controller;
    // Start is called before the first frame update
    void Start()
    {
        controller= GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");  
        //Vector3 vec = controller.velocity;
        //vec.x = x;
        //vec.y = 0f;
        //vec.z = y;
        //controller.AddForce(vec * 5f);
    }
}
