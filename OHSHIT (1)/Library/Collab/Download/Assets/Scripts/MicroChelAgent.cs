using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.MLAgents;
using Unity.MLAgents.Sensors;
using Unity.MLAgents.Actuators;

public class MicroChelAgent : Agent

{
    public int iteration = 0;
    public GraphController controller;
    public int target;
    public int previous;
    public int next;
    public float speed;

    public float[] move;

    // Start is called before the first frame update
    void Start()
    {
        //controller.chels.Add(this);
        move = new float[controller.roadNodes.Count];
        gameObject.SetActive(false);
        gameObject.SetActive(true);


    }

    public override void OnEpisodeBegin()
    {
        iteration++;

        StartCoroutine(ExecuteAfterTime(15.0f, iteration));

        target = Random.Range(0, 5);
    }


    public override void CollectObservations(VectorSensor sensor)
    {
        sensor.AddObservation(transform.localPosition);
        sensor.AddObservation(target);
        sensor.AddObservation(previous);
        sensor.AddObservation(next);
        foreach (MicroChelAgent c in controller.chels) sensor.AddObservation(c.transform.localPosition);
        foreach (MicroChelAgent c in controller.chels) sensor.AddObservation(c.target);
        foreach (MicroChelAgent c in controller.chels) sensor.AddObservation(c.previous);
        foreach (MicroChelAgent c in controller.chels) sensor.AddObservation(c.next);


    }

    public override void OnActionReceived(ActionBuffers actions)
    {
        speed = actions.ContinuousActions[0];
        
        for(int i = 0; i<controller.roadNodes.Count; i++)
        {
            move[i] = actions.ContinuousActions[i + 1];
        }

        GetComponent<Rigidbody2D>().velocity = (controller.roadNodes[next].transform.position - transform.position).normalized* Mathf.Abs(speed)*30;
        
    }



    IEnumerator ExecuteAfterTime(float timeInSec, int x)
    {
        yield return new WaitForSeconds(timeInSec);
        if (x == iteration)
        {
            foreach(MicroChelAgent c in controller.chels)
            {
                c.SetReward(-1.0f);
            }
            EndEpisode();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Node")
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            

            for(int i=0; i<controller.roadNodes.Count; i++)
            {
                if (other.gameObject == controller.roadNodes[i]) previous = i;
            }

            if(previous == target) { 
                SetReward(3f);
                EndEpisode();
            }
            int maxInd = controller.nodesRelative[previous][0];
            float max = move[0];
            for (int i = 0; i < move.Length; i++)
            {
                if ((move[i] > max) && controller.nodesRelative[previous].Contains(i))
                {
                    max = move[i];
                    maxInd = i;
                }
            }

            next = maxInd;

            GetComponent<Collider2D>().isTrigger = true;
        }

    }

    private void OnTriggerExit2D(Collider2D other)
    {
        GetComponent<Collider2D>().isTrigger = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        gameObject.transform.position = controller.roadNodes[previous].transform.position;
        SetReward(-1f);

    }






}
