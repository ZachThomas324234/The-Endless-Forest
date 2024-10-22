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

    //public TestDash td;
    //public gunScript gs;
    //public bullet bullet;

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
        //td = GetComponent<TestDash>();
        //gs = GetComponent<gunScript>();
        //bullet = GetComponent<bullet>();
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

        //if (td.Dashing) return;

        Movement = (CamF * MovementY + CamR * MovementX).normalized;
        rb.AddForce(Movement * Speed);
        rb.AddForce(velocityXZ * counterMovement);

        staminaAmount = Math.Clamp (staminaAmount + (isRunning? -Time.deltaTime: +Time.deltaTime), 0, 2f);

        LockToMaxSpeed();
    }

    public void OnRClick(InputAction.CallbackContext RClick)
    {
        //if(RClick.started)  HoldingRClick = true;
        //if(RClick.canceled) HoldingRClick = false;
        //if (RClick.started && hasRadioactiveNuke && !jumping && !td.Dashing && !crouching && grounded)
        if (RClick.started && hasRadioactiveNuke && !jumping && !crouching && grounded)
        {
            //gs.ableToShoot = false;
            //gs.gunPutAway.Play("gunPutAway");
            armsAppear.Play("armsAppear");
        }

        //if (RClick.canceled && hasRadioactiveNuke && !td.Dashing && !crouching)
        if (RClick.canceled && hasRadioactiveNuke && !crouching)
        {
            //gs.ableToShoot = true;
            jumping = false;
            //gs.gunBringBack.Play("gunBringBack");
            armsPutAway.Play("armsPutAway");
        }
    }

    public void onMove(InputAction.CallbackContext MovementValue)
    {
        Vector2 inputVector = MovementValue.ReadValue<Vector2>();
        MovementX = inputVector.x;
        MovementY = inputVector.y;
    }

    public void Run(InputAction.CallbackContext run)
    {
        //if(run.started && !td.Dashing && !crouching && !cantRun)
        if(run.started && !crouching && !cantRun && staminaAmount >= 0)
        {
            isRunning = true;
            Speed = 60;
        }
        
        //if(run.canceled && !td.Dashing && !crouching)
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

    public void Jump(InputAction.CallbackContext jump)
    {
        //if (jump.started && !jumping && !td.Dashing && !crouching && grounded)
        if (jump.started && !jumping && !crouching && grounded)
        {
            jumping = true;
            rb.linearVelocity = new Vector3 (rb.linearVelocity.x, 0, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * normalJumpForce, ForceMode.VelocityChange);
        }

        //if (jump.canceled && !td.Dashing && !crouching)
        if (jump.canceled && !crouching)
        {
            jumping = false;
            transform.localScale = new Vector3(1, 1, 1);
        }
    }

    public void Crouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            //gs.ableToShoot = false;
            //gs.gunPutAway.Play("gunPutAway");
            crouching = true;
            transform.localScale = new Vector3(1, 0.5f, 1);
            Speed = 20;
        }

        if (context.canceled)
        {
            //gs.ableToShoot = true;
            //gs.gunBringBack.Play("gunBringBack");
            crouching = false;
            transform.localScale = new Vector3(1, 1, 1);
            Speed = 70;
        }
    }

    public void CancelRun()
    {
        cantRun = true;
        Speed = 40;
    }
}
