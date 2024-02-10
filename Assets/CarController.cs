using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CarController : MonoBehaviour
{

    public WheelColliders colliders;
    public float gasInput;
    public float steeringInput;
   
    public float motorPower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

       CheckInput(); 
       applyMotor();
    }


    void CheckInput(){

        gasInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

    }


    void applyMotor(){
        colliders.RRWheels.motorTorque = motorPower * gasInput;
        colliders.RLWheels.motorTorque = motorPower * gasInput;
    }
}




[System.Serializable]
public class WheelColliders{
    public WheelCollider FRWheels;
    public WheelCollider FLWheels;
    public WheelCollider RRWheels;
    public WheelCollider RLWheels;
}

