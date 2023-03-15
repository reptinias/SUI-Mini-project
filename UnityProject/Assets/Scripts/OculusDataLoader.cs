using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusDataLoader : MonoBehaviour
{
    public GameObject head;
    public GameObject handR;
    public GameObject handL;

    public Vector3 headPos;
    public Quaternion headRot;
    public Vector3 handRPos;
    public Vector3 handLPos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        headPos = head.transform.position;
        headRot = head.transform.rotation;
        handRPos = handR.transform.position;
        handLPos = handL.transform.position;
    }

    public Vector3 getHeadPos()
    {
        return headPos;
    }

    public Quaternion getHeadRot()
    {
        return headRot;
    }

    public Vector3 getHandR()
    {
        return handRPos;
    }

    public Vector3 getHandL()
    {
        return handLPos;
    }
}
