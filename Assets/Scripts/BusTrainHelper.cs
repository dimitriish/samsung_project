using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusTrainHelper : MonoBehaviour
{
    public CarController controller;
    public float acceleration = 0.9f;

    Vector3 startingPosition;
    Quaternion startingRotation;
    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
        startingRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        controller.m_verticalInput = acceleration;
        controller.Accelerate();
    }


    public void OnEpisodeBegin()
    {
        gameObject.transform.localPosition = startingPosition;
        gameObject.transform.rotation = startingRotation;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("bus") || collision.gameObject.tag.Equals("nonagent"))
        {
            /*if (transform.position.x < 422 && -395 < transform.position.x)
            {*/
            OnEpisodeBegin();
            /*} else
            {
                EndEpisode();
            }*/
        }


    }
}
