using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogTracking : MonoBehaviour
{
    
    private LoggingManager loggingManager;
    private OculusDataLoader oculusDataLoader;
    private string CsvFileName = "Dataset";
 
    public int classID = 0;

    private Vector3 headPos;
    private Quaternion headRot;
    private Vector3 handRPos;
    private Vector3 handLPos;

    private List<float> coords = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        loggingManager = GameObject.Find("GameManager").GetComponent<LoggingManager>();
        oculusDataLoader = GameObject.Find("GameManager").GetComponent<OculusDataLoader>();
    }

    // Update is called once per frame
    void Update()
    {
        loggingManager.Log(CsvFileName, "Class", classID);

        headPos = oculusDataLoader.getHeadPos();
        headRot = oculusDataLoader.getHeadRot();
        handRPos = oculusDataLoader.getHandR();
        handLPos = oculusDataLoader.getHandL();

        for (int i = 0; i < 3; i++)
        {
            coords.Add(headPos[i]);
        }

        for (int i = 0; i < 4; i++)
        {
            coords.Add(headRot[i]);
        }
        
        for (int i = 0; i < 3; i++)
        {
            coords.Add(handRPos[i]);
        }
        
        for (int i = 0; i < 3; i++)
        {
            coords.Add(handLPos[i]);
        }
        
        for (int i = 0; i < coords.Count; i++)
        {
            loggingManager.Log(CsvFileName, "$Coord " + i.ToString(), coords[i]);
        }
        
        if (Input.GetKey("space"))
        {
            loggingManager.SaveLog(CsvFileName);
            loggingManager.ClearLog(CsvFileName);
            loggingManager.NewFilestamp();
            print("CSV was saved");
        }
    }
}
