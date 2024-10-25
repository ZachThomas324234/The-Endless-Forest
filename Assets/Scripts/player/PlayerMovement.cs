using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Properties")]
    public float Speed, normalJumpForce;
    public float MaxSpeed = 12;
    public float staminaAmount = 0;
    public float counterMovement;
    [Range(0, 2f)]public float staminaCooldown = 0;
    [HideInInspector]public Vector3 CamF;
    [HideInInspector]public Vector3 CamR;
    [HideInInspector]public Vector3 Movement;
    [HideInInspector]public float MovementX;
    [HideInInspector]public float MovementY;

    public Vector3 velocityXZ;

    [Header("References")]
    public Rigidbody rb;
    public Transform Camera;
    public Animator armsPutAway, armsAppear;

    [Header("States")]
    public bool hasDash = false;
    public bool hasRadioactiveNuke = false;
    public bool hasLightningStrike = false;
    public bool hasTeleport = false;
    public bool hasGroundPound = false;
    public bool jumping = false;
    public bool grounded;
    public bool crouching;
    public bool isRunning;
    public bool cantRun;
    public bool HoldingRClick;


    void Awake()
    {
        Camera = GameObject.Find("Main Camera").transform;
        rb = GetComponent<Rigidbody>();
        staminaAmount = 2;
    }

    void FixedUpdate()
    {
        CamF = Camera.forward;
        CamR = Camera.right;
        CamF.y = 0;
        CamR.y = 0;
        CamF = CamF.normalized;
        CamR = CamR.normalized;

        velocityXZ = new Vector3 (rb.linearVelocity.x, 0, rb.linearVelocity.z);

        Movement = (CamF * MovementY + CamR * MovementX).normalized;
        rb.AddForce(Movement * Speed);
        rb.AddForce(velocityXZ * counterMovement);

        staminaAmount = Math.Clamp (staminaAmount + (isRunning? -Time.deltaTime: +Time.deltaTime), 0, 2f);

        LockToMaxSpeed();
    }

    public void onMove(InputAction.CallbackContext MovementValue)
    {
        Vector2 inputVector = MovementValue.ReadValue<Vector2>();
        MovementX = inputVector.x;
        MovementY = inputVector.y;
    }

    public void Run(InputAction.CallbackContext run)
    {
        if(run.started && !crouching && !cantRun && staminaAmount >= 0)
        {
            isRunning = true;
            Speed = 60;
        }
        
        if(run.canceled && !crouching)
        {
            isRunning = false;
            Speed = 40;
        }
    }


    public void LockToMaxSpeed()
    {
        Vector3 newVelocity = rb.linearVelocity;
        newVelocity.y = 0f;
        newVelocity = Vector3.ClampMagnitude(newVelocity, MaxSpeed);
        newVelocity.y = rb.linearVelocity.y;
        rb.linearVelocity = newVelocity;
    }

    public void CancelRun()
    {
        cantRun = true;
        Speed = 40;
    }
}
