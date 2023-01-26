using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Actuators;
using Unity.MLAgents.Sensors;

public class BallAgent : Agent
{
    public Transform target;
    // Start is called before the first frame update

    public override void OnEpisodeBegin()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
        transform.localPosition = new Vector3(Random.Range(-8, 8), 1, Random.Range(-8, 8));

        target.localPosition = new Vector3(Random.Range(-8, 8), 1, Random.Range(-8, 8));
    }

    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target.localPosition);
    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        float moveX = actions.ContinuousActions[0];
        float moveY = actions.ContinuousActions[1];

        GetComponent<Rigidbody>().AddForce(new Vector3(moveX, 0, moveY)*30);

        /*if(Vector3.Distance(target.transform.position, transform.position)<1.5f){
            SetReward(1f);
            EndEpisode();
        }*/

        if (transform.position.y < -2) EndEpisode();
    }

    

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "target")
        {
            SetReward(1f);
            EndEpisode();
        }

        
    }



}
