using Pattern;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
    public const float MIN_MOVE_VALUE_TO_TURN = 0.01f;

    public float MoveValue { get; private set; }
    public float RotateValue { get; private set; }

    public int LiftValue { get; private set; }



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            LiftValue = 1;
        else if (Input.GetMouseButton(1))
            LiftValue = -1;
        else
            LiftValue = 0;

        MoveValue = Input.GetAxis("Vertical");
        RotateValue = Input.GetAxis("Horizontal");
    }
}
