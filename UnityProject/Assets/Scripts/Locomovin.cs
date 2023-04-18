using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Locomovin : MonoBehaviour
{
    public Predictor predictor;
    public float speed = 3.0f; //Controls velocity multiplier
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        //Fetch the Rigidbody component you attach from your GameObject
        rb = GetComponent<Rigidbody>();
        //Set the speed of the GameObject
        speed = 3.0f;
    }
    // Update is called once per frame
    void Update()
    {
        
        if (predictor.predString=="Walking")
        {
            //Move the Rigidbody forwards constantly at speed you define (the blue arrow axis in Scene view)
            rb.velocity = transform.forward * speed;
            Debug.Log("Walking confirmed");
        }
        else {
            rb.velocity = transform.forward * 0;
        }
        
    }
    
}
