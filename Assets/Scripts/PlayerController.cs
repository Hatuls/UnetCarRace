using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] float motorForce = 100f;
    [SerializeField] float breakForce;
    [SerializeField] float maxSteeringAngle;


    private bool isBreaking;
    private float currentBreakForce;
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    Rigidbody _Rb;


    [SerializeField] WheelCollider frontLeftWheelCollider;
    [SerializeField] WheelCollider RearLeftWheelCollider;
    [SerializeField] WheelCollider frontRightWheelCollider;
    [SerializeField] WheelCollider RearRightWheelCollider;



    [SerializeField] Transform frontLeftWheelTransform;
    [SerializeField] Transform RearLeftWheelTransform;
    [SerializeField] Transform frontRightWheelTransform;
    [SerializeField] Transform RearRightWheelTransform;

    private void Start()
    {
        _Rb = GetComponent<Rigidbody>();
        _Rb.centerOfMass = new Vector3(0, -0.9f, 0);
    }



    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
    }

    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(RearLeftWheelCollider, RearLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(RearRightWheelCollider, RearRightWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform transform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        transform.rotation = rot;
        transform.position = pos;
    }

    private void HandleSteering()
    {
        currentSteerAngle = maxSteeringAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void HandleMotor()
    {
        frontLeftWheelCollider.motorTorque = verticalInput * motorForce;
        frontRightWheelCollider.motorTorque = verticalInput * motorForce;

        ApplyBreaking(isBreaking ? breakForce : 0);
    
    }

    private void ApplyBreaking(float breakForce)
    {
        frontRightWheelCollider.brakeTorque = breakForce;
        frontLeftWheelCollider.brakeTorque = breakForce;
        RearLeftWheelCollider.brakeTorque = breakForce;
        RearRightWheelCollider.brakeTorque = breakForce;
    }

    private void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
      verticalInput = Input.GetAxis("Vertical");
        isBreaking = Input.GetKey(KeyCode.Space);


    }
}
