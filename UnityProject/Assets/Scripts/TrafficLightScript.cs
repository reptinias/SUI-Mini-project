using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class TrafficLightScript : MonoBehaviour
{
    private GameObject Red;
    private GameObject Yellow;
    private GameObject Green;

    public Color LightRed;
    public Color LightYellow;
    public Color LightGreen;

    public Color offRed;
    public Color offYellow;
    public Color offGreen;
    
    private float timer = 5.0f;
    private bool changeLight = false;
    private bool changeDown = true;
    private bool yellowActive = false;

    public LogTracking logTracking;
    private int frameCount;


    
    // Start is called before the first frame update
    void Start()
    {
        Red = this.gameObject.transform.GetChild(1).gameObject;
        Yellow = this.gameObject.transform.GetChild(2).gameObject;
        Green = this.gameObject.transform.GetChild(3).gameObject;

        /* offRed = Red.GetComponent<Renderer>();
        offYellow = Yellow.GetComponent<Renderer>();
        offGreen = Green.GetComponent<Renderer>();
         */
        Red.GetComponent<Renderer>().material.color = LightRed;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetButtonDown("Fire1"))
        {
            timer = 0;
            Debug.Log("Pressed");
            changeLight = true;

        }

        if (Input.GetButtonDown("Fire2"))
        {
            
            Debug.Log("Fire2 Pressed");
            

        }

        if (timer >= 1.5 && changeLight)
        {
            if (yellowActive == true)
            {
                yellowActive = false;
                
                if (changeDown == true)
                {
                    Yellow.GetComponent<Renderer>().material.color = offYellow;
                    Green.GetComponent<Renderer>().material.color = LightGreen;
                    logTracking.classID = 1;
                    frameCount = 0;
                    Debug.Log(logTracking.classID);
                    changeLight = false;
                    changeDown = false;
                    
                }

                else if (changeDown == false)
                {
                    Yellow.GetComponent<Renderer>().material.color = offYellow;
                    Red.GetComponent<Renderer>().material.color = LightRed;
                    logTracking.classID = 3;
                    frameCount = 0;
                    Debug.Log(logTracking.classID);
                    changeLight = false;
                    changeDown = true;
                }
            }
            
            else if (yellowActive == false)
            {
                timer = 0;
                Yellow.GetComponent<Renderer>().material.color = LightYellow;
                yellowActive = true;
                frameCount = 0;
                Red.GetComponent<Renderer>().material.color = offRed;
                Green.GetComponent<Renderer>().material.color = offGreen;
            }
        }

        frameCount++;
        if (frameCount == 11)
        {
            if (logTracking.classID == 1)
            {
                logTracking.classID = 2;
                Debug.Log("New ID: " + logTracking.classID);
            }
            if (logTracking.classID == 3)
            {
                logTracking.classID = 0;
                Debug.Log("New ID: " + logTracking.classID);
            }
        }
    }


}
