using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class TargetAgent : Agent
{

    public int iteration = 0;
    public GameObject hunter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void OnEpisodeBegin()
    {
        iteration++;

        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.localPosition = new Vector3(Random.Range(-8, 8), 1, Random.Range(-8, 8));

        hunter.transform.localPosition = new Vector3(Random.Range(-8, 8), 1, Random.Range(-8, 8));

        StartCoroutine(ExecuteAfterTime(10.0f, iteration));

    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(hunter.transform.localPosition);
        sensor.AddObservation(GetComponent<Rigidbody>().velocity);
        sensor.AddObservation(hunter.GetComponent<Rigidbody>().velocity);
        
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        GetComponent<Rigidbody>().AddForce(new Vector3(moveX, 0, moveY) * 30);

        /*if(Vector3.Distance(target.transform.position, transform.position)<1.5f){
            SetReward(1f);
            EndEpisode();
        }*/

        if (hunter.transform.position.y < -2) {
            SetReward(1f);
            EndEpisode();
        }

        if (transform.position.y < -2)
        {
            EndEpisode();
        }

    }



    public void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "hunter")
        {
            EndEpisode();
        }
    }

    IEnumerator ExecuteAfterTime(float timeInSec, int x)
    {
        yield return new WaitForSeconds(timeInSec);
        if (x == iteration)
        {
            SetReward(1f);
            EndEpisode();
        }
    }



    

}
