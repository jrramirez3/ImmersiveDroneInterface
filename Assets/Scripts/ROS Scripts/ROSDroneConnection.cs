﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ROSBridgeLib;
using ROSBridgeLib.std_msgs;
using ROSBridgeLib.interface_msgs;
using UnityEditor;
using System.IO;
using ISAACS;

public class ROSDroneConnection : MonoBehaviour
{
    private ROSBridgeWebSocketConnection ros = null;

    void Start()
    {
        // This is the IP of the linux computer that is connected to the drone.  
        ros = new ROSBridgeWebSocketConnection("ws://10.96.246.159", 9090);
        ros.AddSubscriber(typeof(ObstacleSubscriber));
        ros.AddSubscriber(typeof(EnvironmentSubscriber));
        ros.AddSubscriber(typeof(DronePositionSubscriber));
        ros.AddPublisher(typeof(UserpointPublisher));
        ros.AddServiceResponse(typeof(ROSDroneServiceResponse));
        ros.Connect();
        Debug.Log("Sending connection attempt to ROS");
    }

    // Extremely important to disconnect from ROS. OTherwise packets continue to flow
    void OnApplicationQuit()
    {
        Debug.Log("Disconnecting from ROS");
        if (ros != null)
        {
            ros.Disconnect();
        }
    }

    // Update is called once per frame in Unity
    void Update()
    {
        //print(gameObject.name);
        ros.Render();
    }

    public void PublishWaypointUpdateMessage(UserpointInstruction msg)
    {
        Debug.Log("Published new userpoint instruction: "+ msg.ToYAMLString());
        ros.Publish(UserpointPublisher.GetMessageTopic(), msg);
    }

    public void SendServiceCall(string service, string args)
    {
        Debug.Log("Calling service: " + service);
        ros.CallService(service, args);
    }

}

