using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Barracuda;
using System;
using System.Linq;

public class Predictor : MonoBehaviour
{       
        public NNModel nnmodel;
        private Model _runTimeModel;
        private IWorker _engine;
        public Prediction prediction;
        public string[] predString = new string[2];
        public float predThreshold;
        
        private List<float> coords = new List<float>(new float[143]);
        private Vector3 headPos;
        private Quaternion headRot;
        private Vector3 handRPos;
        private Vector3 handLPos;
        
        private OculusDataLoader oculusDataLoader;
        
        public struct Prediction
        {
            public int predictedValue;
            public float[] predicted;
            public string predictionString;
            public float predThreshold;
            
            public string convertPrediction(int predictionvalue)
            {
                switch (predictionvalue)
                {
                    case -1:
                        predictionString = "Not predictable";
                        break;
                    case 0:
                        predictionString = "Stand still";
                        break;
                    case 1:
                        predictionString = "Starting";
                        break;
                    case 2:
                        predictionString = "Walking";
                        break;
                    case 3:
                        predictionString = "Stopping";
                        break;
                }
    
                return predictionString;
            }
            
            public string SetPrediction(Tensor tensor, int handIndex, float predThreshold)
            {
                predicted = tensor.AsFloats();
                this.predThreshold = predThreshold;
                if (predicted.Max() > this.predThreshold)
                {
                    predictedValue = Array.IndexOf(predicted, predicted.Max());
                }
                else
                {
                    predictedValue = -1;
                }
                //Debug.Log($" hand {handIndex} predicted as {convertPrediction(predictedValue)}");
                return convertPrediction(predictedValue);
            }
        }
        // Start is called before the first frame update
        void Start()
        {
            procesGesture = new bool[] { false, false };
            InputDevices = GameObject.Find("ReadInputs").GetComponent<NewReadInputs>();
            m_hands = InputDevices.m_hands;
            _runTimeModel = ModelLoader.Load(nnmodel);
            _engine = WorkerFactory.CreateWorker(_runTimeModel, WorkerFactory.Device.GPU);
            prediction = new Prediction();  
            
            oculusDataLoader = GameObject.Find("GameManager").GetComponent<OculusDataLoader>();
        }
    
        // Update is called once per frame
        void Update()
        {
            coords = coords.GetRange(13, 142);

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

            //Debug.Log($"hand {handIndex}. coordinates max: {normCoordinates.Max()}. coordinates min: {normCoordinates.Min()}");
            var input = new Tensor(1, 72, coords.ToArray());
            Tensor output = _engine.Execute(input).PeekOutput();
            input.Dispose();
            predString[handIndex] = prediction.SetPrediction(output, handIndex, predThreshold);
            handIndex += 1;
        }
        private void OnDestroy()
        {
            _engine?.Dispose();
        }
}
