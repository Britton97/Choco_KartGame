using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Air_State : State_Base
{
    //REVIEW
    //currentSpeed should not be a variable. The scriptable object should not be holding onto a variable that is constantly changing.
    //elasedTime should not be a variable. The scriptable object should not be holding onto a variable that is constantly changing.
    //should not be reading input actions that should be handled by a monobeaviour script.
    //I'm not sure what dotProduct is doing but it should probably be handled by a monobehaviour script.
    //same with calculatedDotProduct ^^^

    [Header("Exit State Condition")]
    [SerializeField] private float rayDistance;
    [SerializeField] private float applyGravityRayDistance;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private State_Base onTouchGround;
    [SerializeField] private float xTurnSpeedMultiplier = 2f;
    [SerializeField] private float yTurnSpeedMultiplier = 2f;

    private float calculatedDotProduct;
    private float dotProduct;

    private float currentSpeed;
    private float elaspedTime;


    public override void OnEnter(Rigidbody passedRB, GameObject pKartModel, GameObject pKartNormal, GameObject pTiltObject, Kart_Input pInput, Kart_Stats pStats, Player_Stats pPlayerStats)
    {
        base.OnEnter(passedRB, pKartModel, pKartNormal, pTiltObject, pInput, pStats, pPlayerStats);
        elaspedTime = 0;
        kart_stats.canAffectCharge = false;
        //Debug.Log("Flying State");
    }
    public override void AButton()
    {
        float button = input.Kart_Controls.ActionButton.ReadValue<float>(); //read value of A button
        if (button == 1) //if 'A' button held down execute below
        {
            ApplyGravity();
        }
    }

    public void ApplyGravity()
    {
        RaycastHit hit;

        if (!Physics.Raycast(kartNormal.transform.position, Vector3.down, out hit, applyGravityRayDistance, groundLayer))
        {
            rb.AddForce(Vector3.up * _gravity.DataValue * _gravityMultiplier.DataValue * Time.deltaTime, ForceMode.Acceleration);
        }
    }

    public void GroundCheck()
    {
        if (Physics.Raycast(kartNormal.transform.position, Vector3.down, rayDistance, groundLayer))
        {
            //Debug.Log("Exiting Air");
            OnExit(KartState.Ground);
        }
    }

    public void GroundPull()
    {
        //if elaspedTime is greater than flightTime
        //then start evaluating the flightcurve and apply the force
        elaspedTime += Time.deltaTime * (2 - dotProduct);
        if (elaspedTime > kart_stats.flightTime * player_stats.glide)
        {
            float timePassed = elaspedTime - (kart_stats.flightTime * player_stats.glide);
            float gravityPercentage = timePassed / ((kart_stats.flightTime * player_stats.glide) / 2); //after full flight time has elapsed, start applying gravity by the rate of half the flight time
            float curve = kart_stats.flightCurve.Evaluate(gravityPercentage);
            rb.AddForce(Vector3.up * _gravity.DataValue * (_gravityMultiplier.DataValue * curve));
        }

        //kart_stats.boostValue.DataValue = elaspedTime / kart_stats.flightTime;
    }

    public override void LeftStick()
    {
        Vector2 move = input.Kart_Controls.Move.ReadValue<Vector2>();

        float x = kartModel.transform.eulerAngles.x;
        float y = kartModel.transform.eulerAngles.y;
        x = x + (move.y * (kart_stats.turnSpeed * player_stats.turn) * xTurnSpeedMultiplier * Time.deltaTime);
        y = y + (move.x * (kart_stats.turnSpeed * player_stats.turn) * yTurnSpeedMultiplier * Time.deltaTime);
        kartModel.transform.rotation = Quaternion.Euler(x, y, 0);
    }

    public override void Move()
    {
        float button = input.Kart_Controls.ActionButton.ReadValue<float>();

        if (button == 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, kart_stats.topSpeed * player_stats.topSpeed, Time.deltaTime * kart_stats.accelrateRate);
        }
        rb.AddForce((kartModel.transform.forward * currentSpeed * kart_stats.forceMultiplier.DataValue) * calculatedDotProduct);
    }

    public void FlyingAngle()
    {
        dotProduct = Vector3.Dot(Vector3.up ,kartModel.transform.forward);
        dotProduct = 1 - dotProduct;
        //Debug.Log(dotProduct);
        calculatedDotProduct = kart_stats.flightPowerCurve.Evaluate(dotProduct);
        float t = kart_stats.flightPowerCurve.keys[kart_stats.flightPowerCurve.length - 1].value;
    }
}
