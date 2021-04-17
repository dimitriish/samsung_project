using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarInput : MonoBehaviour
{
    // Start is called before the first frame update

    public CarController control;
    
    void Start()
    {
        control = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        control.m_horizontalInput = Input.GetAxis("Horizontal");
        control.m_verticalInput = Input.GetAxis("Vertical");

        control.Accelerate();
        control.Steer();
        
    }
}
