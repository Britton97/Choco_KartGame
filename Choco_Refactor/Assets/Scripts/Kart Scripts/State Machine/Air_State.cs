using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Air_State : State_Base
{
    [SerializeField] PlayerInput playerInput;
    [Header("Exit State Condition")]
    [SerializeField] private LayerMask groundLayer;
    private float elaspedTimeInFlyingState;

    [Header("Lean Settings")]
    [SerializeField] private CharacterRigObjects characterLeanObjects;
    //[SerializeField] CinemachineFreeLook freeLookCamera;
    [SerializeField] CinemachineInputProvider cinemachineInputProvider;

    #region Update and FixedUpdate Functions
    private void Update()
    {
        GroundCheck();
        AButton();
        FlightSlider();
    }

    private void FixedUpdate()
    {
        LeftStick();
        Move();
        Tilt(leftJoystick);
        FlyingAngle();
        GroundPull();
    }
    #endregion

    private Vector2 leftJoystick = Vector2.zero;
    private Vector2 rightJoystick = Vector2.zero;
    private float actionButton = 0;
    public void ReceiveLeftStick(InputAction.CallbackContext context)
    {
        leftJoystick = context.ReadValue<Vector2>();
    }

    public void ReceiveRightStick(InputAction.CallbackContext context)
    {
        rightJoystick = context.ReadValue<Vector2>();
    }

    public void ReceiveActionButton(InputAction.CallbackContext context)
    {
        actionButton = context.ReadValue<float>();
    }
    #region OnEnter Function
    public override void OnEnter(Rigidbody passedRB, GameObject pKartModel, GameObject pKartNormal, GameObject pTiltObject, Kart_Input pInput, Kart_Stats pStats, Player_Stats pPlayerStats)
    {
        base.OnEnter(passedRB, pKartModel, pKartNormal, pTiltObject, pInput, pStats, pPlayerStats);

        playerInput = GetComponent<PlayerInput>();
        int index = playerInput.playerIndex;
        cinemachineInputProvider.PlayerIndex = index;

        elaspedTimeInFlyingState = 0;
        kart_stats.canAffectCharge = false;
    }
    #endregion
    #region AButton Function and Variables
    //private float button;
    public override void AButton()
    {
        //button = input.Kart_Controls.ActionButton.ReadValue<float>(); //read value of A button
        if (actionButton == 1) //if 'A' button held down execute below
        {
            ApplyGravity();
        }
    }
    #endregion
    #region ApplyGravity Function
    [SerializeField] private float applyGravityRayDistance = .05f;
    public void ApplyGravity()
    {
        RaycastHit hit;

        //if ray doesnt hit ground layer then apply gravity
        if (!Physics.Raycast(kartNormal.transform.position, Vector3.down, out hit, applyGravityRayDistance, groundLayer))
        {
            rb.AddForce(Vector3.up * _gravity.DataValue * _gravityMultiplier.DataValue * Time.deltaTime, ForceMode.Acceleration);
        }
    }
    #endregion
    #region GroundCheck Function and Variables
    [Tooltip("The distance a ray will cast. If the ground if less than the distance of this variable you will exit the flying state")]
    [SerializeField] private float groundCheckRayDistance = 1f;
    public void GroundCheck()
    {
        //if ray hit ground layer then exit air state
        if (Physics.Raycast(kartNormal.transform.position, Vector3.down, groundCheckRayDistance, groundLayer))
        {
            Debug.Log("Exiting Air");
            OnExit(KartState.Ground);
        }
    }
    #endregion
    #region GroundPull Function and Variables
    public void GroundPull()
    {
        //if elaspedTime is greater than flightTime
        //then start evaluating the flightcurve and apply the force
        elaspedTimeInFlyingState += Time.deltaTime * (2 - dotProduct);
        if (elaspedTimeInFlyingState > kart_stats.ReturnGlideStat())
        {
            float timePassed = elaspedTimeInFlyingState - kart_stats.ReturnGlideStat();
            float gravityPercentage = timePassed / (kart_stats.ReturnGlideStat() / 2); //after full flight time has elapsed, start applying gravity by the rate of half the flight time
            float curve = kart_stats.flightCurve.Evaluate(gravityPercentage);
            rb.AddForce(Vector3.up * _gravity.DataValue * (_gravityMultiplier.DataValue * curve));
        }

        //kart_stats.boostValue.DataValue = elaspedTime / kart_stats.flightTime;
    }
    #endregion
    #region LeftStick Function and Variables
    [SerializeField] private float xTurnSpeedMultiplier = 1f;
    [SerializeField] private float yTurnSpeedMultiplier = 2f;
    public override void LeftStick()
    {
        //Vector2 move = input.Kart_Controls.Move.ReadValue<Vector2>();

        float x = kartModel.transform.eulerAngles.x;
        float y = kartModel.transform.eulerAngles.y;
        x = x + (leftJoystick.y * (kart_stats.ReturnTurnStat()) * xTurnSpeedMultiplier * Time.deltaTime);
        y = y + (leftJoystick.x * (kart_stats.ReturnTurnStat()) * yTurnSpeedMultiplier * Time.deltaTime);
        kartModel.transform.rotation = Quaternion.Euler(x, y, 0);
    }
    #endregion
    #region Move Function and Variables
    private float currentSpeed;
    public override void Move()
    {
        //float button = input.Kart_Controls.ActionButton.ReadValue<float>();

        if (actionButton == 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, kart_stats.ReturnTopSpeedStat(), Time.deltaTime * kart_stats.baseAccelrateRate);
        }
        rb.AddForce((kartModel.transform.forward * currentSpeed * kart_stats.forceMultiplier.DataValue) * calculatedDotProduct);
    }
    #endregion
    #region FlyingAngle Function and Variables
    private float calculatedDotProduct;
    private float dotProduct;
    public void FlyingAngle()
    {
        dotProduct = Vector3.Dot(Vector3.up ,kartModel.transform.forward);
        dotProduct = 1 - dotProduct;
        calculatedDotProduct = kart_stats.flightPowerCurve.Evaluate(dotProduct);
        //float t = kart_stats.flightPowerCurve.keys[kart_stats.flightPowerCurve.length - 1].value;
    }
    #endregion
    #region FlightSlider Function and Variables
    private void FlightSlider()
    {
        _uiBoostSlider.value = elaspedTimeInFlyingState / kart_stats.baseGllideTime;
    }
    #endregion
}
