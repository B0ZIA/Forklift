using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForkliftMovement : MonoBehaviour
{
    private const float MIN_SPEED_TO_TURN = 0.01f;

    public float power = 3000;
    public float maxSpeed = 50;
    public float maxTurnSpeed = 50;

    private Vector3 relativeVelocity;
    private Vector3 engineForce;

    private float mySpeed;
    private float myTurnSpeed;

    [SerializeField]
    private Transform centerOfMass;
    private Rigidbody rb;
    private InputManager inputs;
    private WheelsRotation wheelRotation;
    private bool reverseRotation;



    private void Start()
    {
        inputs = InputManager.Instance;
        wheelRotation = GetComponent<WheelsRotation>();
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = centerOfMass.localPosition;
    }

    private void Update()
    {
        CarPhysicsUpdate();
    }

    private void LateUpdate()
    {
        wheelRotation.UpdateRotation(relativeVelocity);
    }

    private void FixedUpdate()
    {
        if(mySpeed < maxSpeed)
        {
            Move();
        }
        
        if (mySpeed > MIN_SPEED_TO_TURN && inputs.MoveValue > InputManager.MIN_MOVE_VALUE_TO_TURN || inputs.MoveValue < -InputManager.MIN_MOVE_VALUE_TO_TURN)
        {
            if(myTurnSpeed < maxTurnSpeed)
            {
                Turn();
            }
        }
    }

    private void Move()
    {
        rb.AddForce(engineForce * Time.deltaTime);
    }

    private void Turn()
    {
        int inverseMultiplier = reverseRotation ? -1 : 1;
        rb.AddTorque(transform.up * power * inputs.RotateValue * inverseMultiplier * Time.deltaTime, ForceMode.Acceleration);
    }

    void CarPhysicsUpdate()
    {
        Vector3 flatVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        Vector3 direction = transform.TransformDirection(Vector3.forward);
        Vector3 flatDirection = Vector3.Normalize(new Vector3(direction.x, 0, direction.z));

        relativeVelocity = transform.InverseTransformDirection(flatVelocity);
        mySpeed = flatVelocity.magnitude;
        myTurnSpeed = rb.angularVelocity.magnitude;
        engineForce = flatDirection * (power * inputs.MoveValue) * rb.mass;
    }
}
