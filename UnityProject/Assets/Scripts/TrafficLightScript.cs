using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

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
        if (Input.GetKey("Enter"))
        {
            timer = 0;
        }

        if (timer >= 1)
        {
            if (yellowActive == true)
            {
                if (changeDown == true)
                {
                    
                }

                if (changeDown == false)
                {
                    
                }
            }

            if (yellowActive == false)
            {
                
            }
        }
    }
}
