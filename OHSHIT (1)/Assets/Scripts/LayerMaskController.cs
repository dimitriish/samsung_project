using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;
public class LayerMaskController : MonoBehaviour
{
    public DataController dataController;
    public int busIndex;
    RayPerceptionSensorComponent3D[] raySensors;

    public GameObject [] checks;
    void Awake()
    {
        dataController.buses.Add(gameObject);
        busIndex = dataController.buses.IndexOf(gameObject);

        new Layers().AddNewLayer("bus" + busIndex);
        gameObject.layer = LayerMask.NameToLayer("bus" + busIndex);

        checks = GetComponent<BusAgent>().checks;
        new Layers().AddNewLayer("checkpoint" + busIndex);
        foreach(GameObject go in checks){
            go.layer = LayerMask.NameToLayer("checkpoint" + busIndex);
        }

        raySensors = GetComponents<RayPerceptionSensorComponent3D>();

        Debug.Log(raySensors.Length);
      
    }


    private void Start()
    {
        foreach (RayPerceptionSensorComponent3D raySensor in raySensors)
        {
            if (raySensor.SensorName.Equals("RoundBusSensor") || raySensor.SensorName.Equals("FrontBusSensor"))
            {
                foreach (GameObject b in dataController.buses)
                {
                    if (dataController.buses.IndexOf(b) != busIndex)
                    {
                        raySensor.RayLayerMask |= 1 << LayerMask.NameToLayer("bus" + dataController.buses.IndexOf(b));
                    }
                }

            }

            if (raySensor.SensorName.Equals("RoundCheckpointSensor") || raySensor.SensorName.Equals("FrontCheckpointSensor"))
            {
                raySensor.RayLayerMask |= 1 << LayerMask.NameToLayer("checkpoint" + busIndex);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
