using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public enum GearState
{
    Neutral,
    Running,
    CheckingChange,
    Changing
};
public class FRMuscleController : MonoBehaviour
{
    
    private Rigidbody rb;
    public WheelCollider frWheelCollider;
    public WheelCollider flWheelCollider;
    public WheelCollider rrWheelCollider;
    public WheelCollider rlWheelCollider;

    
    public Rigidbody frWheel;
    public Rigidbody flWheel;
    public Rigidbody rrWheel;
    public Rigidbody rlWheel;
    public Rigidbody frKnuckle;
    public Rigidbody flKnuckle;
    public Rigidbody rrKnuckle;
    public Rigidbody rlKnuckle;
    public float gasInput;

    public float brakeInput;

    public GameObject frTarg;
    public GameObject flTarg;
    public GameObject rrTarg;
    public GameObject rlTarg;

    /*public GameObject aTarg;
    public GameObject knTarg;
    public GameObject frkTarg;
    public GameObject flkTarg;
    public Rigidbody frBrake;
    public Rigidbody flBrake;*/
    

    public float steeringInput;
    public float motorPower = 500;
    public float brakePower;
    public float speed;
    public float speedClamped;
    public AnimationCurve steeringCurve;
    
    public float maxEngineTorque = 638f; // Maximum engine torque
    //public AnimationCurve torqueCurve; // Torque curve for the engine
    public AnimationCurve powerCurve; // Power curve for the engine
    public float[] gearRatios; // Array of gear ratios  
    public float idleRPM = 800f;
    public float rpm; // Current engine RPM
    public float redLine = 6112f;
    public int currentGear = 0; // Current gear

    public int isEngineRunning;
    public float differentialRatio;
    //private float currentTorque;
    public float clutch;
    private float wheelRPM;
    public GearState gearState;
    public float increaseGearRPM;
    public float decreaseGearRPM;
    public float changeGearTime=0.5f;
    public float movingDirection;



    /*private void SyncTargets(GameObject armTarg, GameObject kTarg){
        armTarg.transform.position = kTarg.transform.position;
    }
    */

    private void SynchronizeRearWheelPosition(WheelCollider wheelCollider, Rigidbody visualWheel, Rigidbody knuckle, GameObject targ)
    {
        
        // Apply the calculated position to the visual wheel's and knuckle's transform
        Vector3 pos;
        Quaternion quat;
        wheelCollider.GetWorldPose(out pos, out quat);
        //visualWheel.transform.position = new Vector3(visualWheel.transform.position.x, pos.y, visualWheel.transform.position.z);
        knuckle.transform.position = targ.transform.position;
        visualWheel.transform.position = pos;
        visualWheel.transform.rotation = quat;
        
    }

    private void SynchronizeFrontWheelPosition(WheelCollider wheelColl, Rigidbody wheelMdl, Rigidbody knMdl, GameObject targ)
    {
        
        // Apply the calculated position to the visual wheel's and knuckle's transform
        Vector3 pos;
        Quaternion quat;
        wheelColl.GetWorldPose(out pos, out quat);
        wheelMdl.transform.position = new Vector3(wheelMdl.transform.position.x, pos.y, wheelMdl.transform.position.z);
        //wheelMdl.transform.position = pos;
        wheelMdl.transform.rotation = quat;
        
        //brake.transform.rotation = new Quaternion(brake.transform.rotation.x, brake.transform.rotation.y, wheelMdl.transform.rotation.z, brake.transform.rotation.w);
        knMdl.transform.position = targ.transform.position;
        //knMdl.transform.rotation = new Quaternion(knMdl.transform.rotation.x, knMdl.transform.rotation.y, wheelMdl.transform.rotation.z, knMdl.transform.rotation.w);

        //knMdl.transform.rotation = new Quaternion(knMdl.transform.rotation.x, quat.y, knMdl.transform.rotation.z, knMdl.transform.rotation.w);
        //knMdl.transform.position = new Vector3(knMdl.transform.position.x, pos.y, wheelColl.transform.position.z);
        //wheelMdl.transform.rotation = new Quaternion(wheelMdl.transform.rotation.x, wheelMdl.transform.rotation.y, wheelMdl.transform.rotation.z, wheelMdl.transform.rotation.w);
        //wheelMdl.transform.rotation = new Quaternion(quat.x, wheelMdl.transform.rotation.y, wheelMdl.transform.rotation.z, quat.w);


        
    }

    
    // Start is called before the first frame update
    void Start()
    {   
        rb = gameObject.GetComponent<Rigidbody>();
        isEngineRunning = 1;
    }

    // Update is called once per frame
    void Update()
    {
        SynchronizeFrontWheelPosition(frWheelCollider, frWheel, frKnuckle,frTarg);
        SynchronizeFrontWheelPosition(flWheelCollider, flWheel, flKnuckle,flTarg);
        SynchronizeRearWheelPosition(rrWheelCollider, rrWheel, rrKnuckle, rrTarg);
        SynchronizeRearWheelPosition(rlWheelCollider, rlWheel, rlKnuckle, rlTarg);
        //SyncTargets(aTarg, knTarg);
        /*SyncTargets(frArm,frTarg);
        SyncTargets(flArm,flTarg);
        SyncTargets(rrArm,rrTarg);
        SyncTargets(rlArm,rlTarg);*/
        speed = rrWheelCollider.rpm*rrWheelCollider.radius*2f*Mathf.PI /10f;
        speedClamped = Mathf.Lerp(speedClamped, speed, Time.deltaTime);
        //speed = CalculateSpeed();
        //UpdateGearbox();
        CheckInput(); 
        ApplyMotor();
        ApplySteering();
        ApplyBrakes();
        
    }
    /*private float CalculateRPM()
    {
     if (gasInput == 0)
    {
        return idleRPM;
    }
    else
    {
        // Calculate engine RPM based on throttle input
        float maxRPM = 6112f; // Set your maximum RPM here
        float rpm = maxRPM * gasInput;
        return Mathf.Clamp(rpm, 0f, maxRPM);
    }
    }*/

    private float CalculateTorque(/*float rpm*/)
{
    {
        float torque = 0;
        if (rpm < idleRPM + 200 && gasInput==0 && currentGear == 0)
        {
            gearState = GearState.Neutral;
        }
        if (gearState == GearState.Running && clutch > 0)
        {
            if (rpm > increaseGearRPM)
            {
                StartCoroutine(ChangeGear(1));
            }
            else if (rpm < decreaseGearRPM)
            {
                StartCoroutine(ChangeGear(-1));
            }
        }
        if (isEngineRunning > 0)
        {
            if (clutch < 0.1f)
            {
                rpm = Mathf.Lerp(rpm, Mathf.Max(idleRPM, redLine * gasInput), Time.deltaTime * 3f);
            }
            else
            {
                wheelRPM = Mathf.Abs((rrWheelCollider.rpm + rlWheelCollider.rpm) / 2f) * gearRatios[currentGear] * differentialRatio;
                rpm = Mathf.Lerp(rpm, Mathf.Max(idleRPM - 100, wheelRPM), Time.deltaTime * 3f);
                torque = (powerCurve.Evaluate(rpm / redLine) * motorPower / rpm) * gearRatios[currentGear] * differentialRatio * 6 * clutch;
            }
        }
        return torque;
    }
    //return torqueCurve.Evaluate(rpm);
}

    IEnumerator ChangeGear(int gearChange)
    {
        gearState = GearState.CheckingChange;
        if (currentGear + gearChange >= 0)
        {
            if (gearChange > 0)
            {
                //increase the gear
                yield return new WaitForSeconds(0.7f);
                if (rpm < increaseGearRPM || currentGear >= gearRatios.Length - 1)
                {
                    gearState = GearState.Running;
                    yield break;
                }
            }
            if (gearChange < 0)
            {
                //decrease the gear
                yield return new WaitForSeconds(0.1f);

                if (rpm > decreaseGearRPM || currentGear <= 0)
                {
                    gearState = GearState.Running;
                    yield break;
                }
            }
            gearState = GearState.Changing;
            yield return new WaitForSeconds(changeGearTime);
            currentGear += gearChange;
        }

        if(gearState!=GearState.Neutral)
        gearState = GearState.Running;
    }

    private float CalculateSpeed()
    {
        // Calculate speed based on current gear and engine RPM
        float wheelRadius = 0.38f; // Assuming wheel radius is 0.5 meters
        float angularVelocity = rpm * 2 * Mathf.PI / 60; // Convert RPM to radians per second
        float speed = angularVelocity * wheelRadius; // Calculate linear speed
        return speed;
    }

    /*private void UpdateGearbox(){
        if (currentGear < maxGears && rpm > minRPMToShiftUp && speed > 0)
        {
            currentGear++;
        }
        else if (currentGear > 0 && rpm < maxRPMToShiftDown)
        {
            currentGear--;
        }
    }*/

    void CheckInput(){
        gasInput = Input.GetAxis("Vertical");
        /*if (gasPedal.isPressed)
        {
            gasInput += gasPedal.dampenPress;
        }
        if (brakePedal.isPressed)
        {
            gasInput -= brakePedal.dampenPress;
        }*/
        if (Input.GetKey(KeyCode.S)){
            gasInput -= brakeInput;
        }
        if (Mathf.Abs(gasInput) > 0&&isEngineRunning==0)
        {
            //StartCoroutine(GetComponent<EngineAudio>().StartEngine());
            gearState = GearState.Running;
        }
        steeringInput = Input.GetAxis("Horizontal");

        movingDirection = Vector3.Dot(transform.forward, rb.velocity);
        if (gearState != GearState.Changing)
        {
            if (gearState == GearState.Neutral)
            {
                clutch = 0;
                if (Mathf.Abs( gasInput )> 0) gearState = GearState.Running;
            }
            else
            {
            clutch = Input.GetKey(KeyCode.LeftShift) ? 0 : Mathf.Lerp(clutch, 1, Time.deltaTime);
            }
        }
        else
        {
            clutch = 0;
        }
        if (movingDirection < -0.5f && gasInput > 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else if (movingDirection > 0.5f && gasInput < 0)
        {
            brakeInput = Mathf.Abs(gasInput);
        }
        else
        {
            brakeInput = 0;
        }
        

    }

    void ApplyMotor(){
        float torque = CalculateTorque(/*rpm*/);
        rlWheelCollider.motorTorque = torque;//motorPower * gasInput;
        rrWheelCollider.motorTorque = torque;//motorPower * gasInput;
    }

    void ApplySteering(){

    float steeringAngle = steeringInput * steeringCurve.Evaluate(speed) * 40;
    frWheelCollider.steerAngle = steeringAngle;
    flWheelCollider.steerAngle = steeringAngle;
    
    }

    void ApplyBrakes(){

        frWheelCollider.brakeTorque = brakeInput * brakePower* 0.8f ;
        flWheelCollider.brakeTorque = brakeInput * brakePower * 0.8f;
        rrWheelCollider.brakeTorque = brakeInput * brakePower * 0.5f;
        rlWheelCollider.brakeTorque = brakeInput * brakePower *0.5f;

    }
}
