using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class BusAgent : Agent
{
    public LayerMaskController layerMaskController;
    public CarController controller;
    public GameObject[] checks;
    public List<GameObject> ListChecks;
   

    int iteration = 0;
    public Rigidbody rBody;
    Vector3 startingPosition;
    Quaternion startingRotation;

    

    // Start is called before the first frame update`
    public override void Initialize()
    {
        ListChecks = new List<GameObject>(checks);
        rBody = GetComponent<Rigidbody>();
        startingPosition = transform.localPosition;
        startingRotation = transform.rotation;
        controller = GetComponent<CarController>();
        
    }

    

    public override void OnEpisodeBegin()
    {
        iteration++;

        foreach(GameObject g in checks)
        {
            g.SetActive(true);
        }

        gameObject.transform.localPosition = startingPosition;
        gameObject.transform.rotation = startingRotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        StartCoroutine(ExecuteAfterTime(250, iteration));

    }

    public override void CollectObservations(VectorSensor sensor)
    {
       
        sensor.AddObservation(GetComponent<Rigidbody>().velocity);

    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        controller.m_horizontalInput = actions.DiscreteActions[0]-1;
        controller.m_verticalInput = actions.ContinuousActions[0];
        controller.Steer();
        controller.Accelerate();


       if(transform.localPosition.y < -10)
        {
            SetReward(-1f);
            EndEpisode();
        }

      
    }

    public override void Heuristic(float[] actionsOut)
    {
        actionsOut[0]= Input.GetAxis("Horizontal");
        actionsOut[1]= Input.GetAxis("Vertical");
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("checkpoint") && (ListChecks.Contains(other.gameObject)))
        {
            other.gameObject.SetActive(false);
            SetReward(0.4f);

            if (other.name.Equals("last"))
            {
                SetReward(2f);
                EndEpisode();
            }
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("bus") || collision.gameObject.tag.Equals("nonagent"))
        {
            /*if (transform.position.x < 422 && -395 < transform.position.x)
            {*/
                SetReward(-1f);
                EndEpisode();
            /*} else
            {
                EndEpisode();
            }*/
        }


    }

    IEnumerator ExecuteAfterTime(float timeInSec, int x)
    {
        yield return new WaitForSeconds(timeInSec);
        if (x == iteration)
        {
            SetReward(-0.05f);
            EndEpisode();
        }
    }

    





}
