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

    private Color oldRed;
    private Color oldYellow;
    private Color oldGreen;
    
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

        oldRed = Red.GetComponent<Renderer>().material.color;
        oldYellow = Yellow.GetComponent<Renderer>().material.color;
        oldGreen = Green.GetComponent<Renderer>().material.color;

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

        if (timer >= 1 && changeLight)
        {
            if (yellowActive == true)
            {
                yellowActive = false;
                
                if (changeDown == true)
                {
                    Yellow.GetComponent<Renderer>().material.color = oldYellow;
                    Green.GetComponent<Renderer>().material.color = LightGreen;
                    logTracking.classID = 1;
                    frameCount = 0;
                    Debug.Log(logTracking.classID);
                    changeLight = false;
                    changeDown = false;
                    
                }

                if (changeDown == false)
                {
                    Yellow.GetComponent<Renderer>().material.color = oldYellow;
                    Red.GetComponent<Renderer>().material.color = LightRed;
                    logTracking.classID = 3;
                    frameCount = 0;
                    Debug.Log(logTracking.classID);
                    changeLight = false;
                    changeDown = true;
                }
            }
            
            if (yellowActive == false)
            {
                timer = 0;
                Yellow.GetComponent<Renderer>().material.color = LightYellow;
                yellowActive = true;
                Red.GetComponent<Renderer>().material.color = oldRed;
                Green.GetComponent<Renderer>().material.color = oldGreen;
            }
        }

        frameCount++;
        if (frameCount == 11)
        {
            if (logTracking.classID == 1)
            {
                logTracking.classID = 2;
                Debug.Log(logTracking.classID);
            }
            if (logTracking.classID == 3)
            {
                logTracking.classID = 0;
                Debug.Log(logTracking.classID);
            }
        }
    }
}
