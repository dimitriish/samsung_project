using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadController : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("bus")) {
            BusAgent bus;
            bus = other.GetComponent<BusAgent>();
            bus.SetReward(-1f);
            bus.EndEpisode();
        }

        if (other.tag.Equals("nonagent"))
        {
           
            other.GetComponent<BusTrainHelper>().OnEpisodeBegin();
        }
    }
}
