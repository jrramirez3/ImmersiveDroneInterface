namespace ISAACS
{
    using UnityEngine;
    using UnityEngine.UI; // <-- you need this to access UI (button in this case) functionalities
    using ROSBridgeLib.interface_msgs;
    using VRTK;
    using System.Collections;
    using System.Collections.Generic;
    using System.IO;

    public class LaunchButton : MonoBehaviour
    {
        Button myButton;
        bool flying;
        private GameObject controller; //needed to access pointer
        GameObject drone_obj;

        void Awake()
        {

            
            controller = GameObject.FindGameObjectWithTag("GameController");
            //controller = GameObject.Find("controller_left");

            myButton = GetComponent<Button>(); // <-- you get access to the button component here

            myButton.onClick.AddListener(() => { OnClickEvent(); });  // <-- you assign a method to the button OnClick event here
            
            flying = false;
        }

        private void Update()
        {
            //print(gameObject.name);
            if (flying)
            {
                WorldProperties.runtime += Time.deltaTime;
                
            }

        }

        void OnClickEvent()
        {
            
            if (controller.GetComponent<VRTK_Pointer>().IsPointerActive())
            {
                
                if (WorldProperties.selectedDrone != null && !flying)
                {
                    WorldProperties.worldObject.GetComponent<ROSDroneConnection>().SendServiceCall("/takeoff", "");
                    WorldProperties.worldObject.GetComponent<MoveDrone>().move = true;
                    Debug.Log("Moving Drone");

                    flying = true;
                    Debug.Log("Total planning time was: " + WorldProperties.planningTime + "s");

                }

                else if (WorldProperties.selectedDrone != null && flying)
                {
                    WorldProperties.worldObject.GetComponent<ROSDroneConnection>().SendServiceCall("/land", "");
                    GetComponentInChildren<Text>().text = "Takeoff";
                    flying = false;
                    Debug.Log("Total flight time was: " + WorldProperties.runtime + "s");

                }
                
               
            }        
        }

        void OnApplicationQuit()
        {
            if (WorldProperties.selectedDrone != null && flying)
            {
                WorldProperties.worldObject.GetComponent<ROSDroneConnection>().SendServiceCall("/land", "");
            }
        }
    }
}