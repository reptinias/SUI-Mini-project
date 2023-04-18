using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OculusDataLoader : MonoBehaviour
{
    public GameObject head;
    public GameObject handR;
    public GameObject handL;
    public GameObject xrObject;

    public Vector3 headPos;
    public Quaternion headRot;
    public Vector3 handRPos;
    public Vector3 handLPos;
    public Vector3 xrRig;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(getHeadRot() + ", " + getHeadPos() + ", " + getHandR() + ", " + getHandL());
        
        //Debug.Log(headRot);
        xrRig = xrObject.transform.position;
        headPos = head.transform.position;
        headRot = head.transform.rotation;
        handRPos = handR.transform.position;
        handLPos = handL.transform.position;
        
    }

    public Vector3 getHeadPos()
    {
        head.transform.RotateAround(xrRig, Vector3.up, -headRot.y);
        return head.transform.position-xrRig;
    }

    public Quaternion getHeadRot()
    {
        //headRot = Quaternion.Euler(headRot.x, 0, headRot.z);
        return headRot;
    }

    public Vector3 getHandR()
    {
        handR.transform.RotateAround(xrRig, Vector3.up, -headRot.y);
        return handR.transform.position-xrRig;
    }

    public Vector3 getHandL()
    {
        handL.transform.RotateAround(xrRig, Vector3.up, -headRot.y);
        return handL.transform.position-xrRig;
    }
}
