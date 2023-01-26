using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{

    float vertical;
    float horizontal;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if(GetComponent<Rigidbody>().velocity.magnitude<15) GetComponent<Rigidbody>().AddForce(new Vector3(horizontal, 0, vertical)*15);
    }
}
