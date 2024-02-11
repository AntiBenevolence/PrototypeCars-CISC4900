using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class CarController : MonoBehaviour
{
    private Rigidbody rb;
    public WheelColliders colliders;
    public float gasInput;
    public float steeringInput;
    public float motorPower;

    private float speed;

    public AnimationCurve steeringCurve;


    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        speed = rb.velocity.magnitude;
        CheckInput(); 
        ApplyMotor();
        ApplySteering();
    }


    void CheckInput(){

        gasInput = Input.GetAxis("Vertical");
        steeringInput = Input.GetAxis("Horizontal");

    }


    void ApplyMotor(){
        colliders.RRWheels.motorTorque = motorPower * gasInput;
        colliders.RLWheels.motorTorque = motorPower * gasInput;
    }


    void ApplySteering(){

    float steeringAngle = steeringInput * steeringCurve.Evaluate(speed);
    colliders.FRWheels.steerAngle = steeringAngle;
    colliders.FLWheels.steerAngle = steeringAngle;
    
}

}




[System.Serializable]
public class WheelColliders{
    public WheelCollider FRWheels;
    public WheelCollider FLWheels;
    public WheelCollider RRWheels;
    public WheelCollider RLWheels;
}

