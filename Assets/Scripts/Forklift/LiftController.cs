using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LiftController : MonoBehaviour
{
    private const float MIN_LIFT_VALUE = -0.55f;
    private const float MAX_LIFT_VALUE = 0.65f;

    public Action OnForceDrop;
    public float maxElevateSpeed;
    public float power;

    private float speed;
    private Vector3 startPos;
    private Rigidbody rb;
    private InputManager inputs;



    void Start()
    {
        inputs = InputManager.Instance;
        rb = GetComponent<Rigidbody>();
        startPos = transform.localPosition;
    }

    private void Update()
    {
        speed = rb.velocity.magnitude;
        transform.localPosition = new Vector3(startPos.x, Mathf.Clamp(transform.localPosition.y, MIN_LIFT_VALUE, MAX_LIFT_VALUE), startPos.z);

        if (transform.localPosition.y < MAX_LIFT_VALUE && transform.localPosition.y > MIN_LIFT_VALUE)
            rb.isKinematic = inputs.LiftValue == 0;
        else
        {
            if(transform.localPosition.y >= MAX_LIFT_VALUE)
            {
                if(inputs.LiftValue > 0)
                    rb.isKinematic = true;
                else if (inputs.LiftValue < 0)
                    rb.isKinematic = false;
            }
            else if (transform.localPosition.y <= MIN_LIFT_VALUE)
            {
                if (inputs.LiftValue < 0)
                {
                    OnForceDrop.Invoke();
                    rb.isKinematic = true;
                }
                else if (inputs.LiftValue > 0)
                    rb.isKinematic = false;
            }
        }

        transform.localEulerAngles = Vector3.zero;
    }

    void FixedUpdate()
    {
        if(inputs.LiftValue != 0 && speed < maxElevateSpeed)
            rb.AddForce(Vector3.up * power * inputs.LiftValue *  Time.deltaTime);
    }
}
