using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;


public class Ground_State : State_Base
{
    [Header("Ground State Variables")]
    [SerializeField] private float rayDistance = 1f; // Distance of the raycast used to detect the ground.
    [SerializeField] private float applyGravityRayDistance; // Distance of the raycast used to apply gravity.
    [SerializeField] private float wallRayDistance; // Distance of the raycast used to detect walls.
    [Header("Ground Detection Layer")]
    [SerializeField] private LayerMask groundLayer; // Layer mask used to detect the ground.
    [SerializeField] private VisualEffect boostEffect;
    [SerializeField] private VisualEffect driftEffect;
    private float currentSpeed; // The current speed of the kart.

    #region WaveEffect (Commented Out)
    /*
    [Header("Wave Effect Settings")]
    [SerializeField] private GameObject waveEffectPrefab; // The prefab for the wave effect.
    private GameObject waveEffect; // The instance of the wave effect.
    [SerializeField] private Vector3 waveOffset = new Vector3(0, 0, 1.8f); // The offset of the wave effect from the kart.
    private WaveController waveController; // The controller for the wave effect.
    */
    #endregion

    private void Update()
    {
        AButton();
        //boostEffect.SetFloat("BoostPower", boostPercentageToFull);
    }

    private void FixedUpdate()
    {
        AirCheck();
        SetDriveMomentumDirection();
        SpeedTurnLimiter();
        LeftStick();
        MatchNormal();
        ApplyGravity();
        Move();
        Tilt();
        WallCheck();
        BoardIdleSine();
        UpdateChargeMeter();
        SetBooster();
        EnableorDisableDriftEffect();
    }

    #region OnEnter and OnExit Functions
    public override void OnEnter(Rigidbody passedRB, GameObject pKartModel, GameObject pKartNormal, GameObject pTiltObject, Kart_Input pInput, Kart_Stats pStats, Player_Stats pPlayerStats)
    {

        base.OnEnter(passedRB, pKartModel, pKartNormal, pTiltObject, pInput, pStats, pPlayerStats);
        if (passedRB == null)
        {
            Debug.LogError($"nothing was passed");
        }

        kartModel.transform.localEulerAngles = new Vector3(0, kartModel.transform.localEulerAngles.y, kartModel.transform.localEulerAngles.z);
        currentSpeed = 0f;
        #region WaveEffect Logic (Commented Out)
        /*
        if (waveEffect == null)
        {
            waveEffect = Instantiate(waveEffectPrefab, kartModel.transform.position, Quaternion.identity);
            waveEffect.transform.parent = kartModel.transform;
            waveEffect.transform.localPosition = waveOffset;
            waveEffect.transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 0));
            waveController = waveEffect.GetComponent<WaveController>();
            waveEffect.SetActive(true);
        }

        waveEffect.SetActive(true);
        */
        #endregion
        kart_stats.canAffectCharge = true;
    }

    public override void OnExit(KartState passIn)
    {
        base.OnExit(passIn);
        #region WaveEffect Logic (Commented Out)
        //waveEffect.SetActive(false);
        #endregion
    }
    #endregion
    #region Aircheck Function Variables and Functions
    public void AirCheck()
    {
        Debug.DrawRay(kartNormal.transform.position, Vector3.down * rayDistance, Color.red);
        if (!Physics.Raycast(kartNormal.transform.position, Vector3.down, rayDistance, groundLayer))
        {
            OnExit(KartState.Flying);
            Debug.Log("Trying to exit ground state");
        }
    }
    #endregion
    #region Left Stick Function Variables and Functions
    private float rotate;
    private float currentRotate;
    public override void LeftStick()
    {
        Vector2 move = input.Kart_Controls.Move.ReadValue<Vector2>();
        rotate = move.x * (kart_stats.ReturnTurnStat());
        currentRotate = Mathf.Lerp(currentRotate, rotate, Time.deltaTime * 4f);
        float turnLimiter = Mathf.Abs(currentSpeed / kart_stats.ReturnTopSpeedStat());
        //currentRotate = currentRotate * kart_stats.turnSpeedLimiterCurve.Evaluate(turnLimiter);
        rotate = 0f;

        //needs to lerp between kartModel.transform.localEulerAngles and the newRotation for the blend controls to work
        Vector3 currentRotation = kartModel.transform.localEulerAngles;
        Vector3 newRotation = new Vector3(0f, kartModel.transform.localEulerAngles.y + currentRotate, 0);
        Vector3 blendRotation = Vector3.Lerp(currentRotation, newRotation, playerInputDirectionBlendAmount); //0 will not let the player turn / 1 will resume normal turning
        kartModel.transform.localEulerAngles = Vector3.Lerp(currentRotation, blendRotation, Time.deltaTime * 4f);
        #region WaveEffect Logic (Commented Out)
        //waveController.ReceiveVector2Input(move);
        #endregion
    }
    #endregion
    #region Apply Gravity Function Variables and Functions
    public void ApplyGravity()
    {
        RaycastHit hit;

        if (!Physics.Raycast(kartNormal.transform.position, Vector3.down, out hit, applyGravityRayDistance, groundLayer))
        {
            rb.AddForce(Vector3.up * _gravity.DataValue * _gravityMultiplier.DataValue * Time.deltaTime, ForceMode.Acceleration);
        }
    }
    #endregion
    #region Match Normal Function Variables and Functions
    [Header("Match Ground Normal Speed")]
    [Range(0,20)]
    [SerializeField] private float _matchNormalSpeed = 8.0f;
    public void MatchNormal()
    {
        RaycastHit hitNear;
        Physics.Raycast(kartNormal.transform.position, Vector3.down, out hitNear, rayDistance);
        kartNormal.transform.up = Vector3.Lerp(kartNormal.transform.up, hitNear.normal, Time.deltaTime * _matchNormalSpeed);
        kartNormal.transform.Rotate(0, kartNormal.transform.parent.transform.eulerAngles.y, 0);
    }
    #endregion
    #region AButton Function Variables and Functions
    private bool lastFrameForButtonDown = false;
    public override void AButton()
    {
        float button = input.Kart_Controls.ActionButton.ReadValue<float>();

        if (button == 1) //button held down == 1
        {
            Vector2 move = input.Kart_Controls.Move.ReadValue<Vector2>();

            currentSpeed = Mathf.Lerp(currentSpeed, 0, Time.deltaTime * (kart_stats.ReturnWeightStat()));

            if (!lastFrameForButtonDown)
            {
                SetDriftMomentumDirection(); //Testing!!!
                if (!boostChargeUpCoroutineRunning) boostChargeUpCoroutine = StartCoroutine(ChargeUpBoostCoroutine());
            }
            lastFrameForButtonDown = true;

            if (currentSpeed < 0) { currentSpeed = 0; }

            if (move.y < -.95)
            {
                //OnExit(KartState.Ground);
                //kartController.OnLeaveKartEvent();
                Debug.Log("Trying to exit the kart");
            }
            else if (move.y > 0.25f && currentSpeed <= kart_stats.scootSpeed.DataValue)
            {
                currentSpeed = kart_stats.scootSpeed.DataValue;
            }
        }
        else if (button == 0) //button up == 0
        {
            if (lastFrameForButtonDown)
            {
                //flip bool
                lastFrameForButtonDown = false;

                boostChargeUpCoroutineRunning = false;
                StopCoroutine(boostChargeUpCoroutine);

                boostDechargeCoroutine = StartCoroutine(DechargeBoostCoroutine());
            }
        }

    }
    #endregion
    #region Boost Coroutine Variables and Functions
    #region Charge Up Boost Coroutine Variables and Functions

    private Coroutine boostChargeUpCoroutine;
    private bool boostChargeUpCoroutineRunning = false;
    private WaitForFixedUpdate waitForFixedUpdateBoost = new WaitForFixedUpdate();
    private float chargeUpBoostTime;
    private float boostPercentageToFull;
    private float boostPercentageToFullUI;
    IEnumerator ChargeUpBoostCoroutine()
    {
        boostChargeUpCoroutineRunning = true;
        chargeUpBoostTime = 0;
        while (chargeUpBoostTime < kart_stats.ReturnChargeStat())
        {
            chargeUpBoostTime += Time.deltaTime;
            boostPercentageToFull = chargeUpBoostTime / kart_stats.ReturnChargeStat();
            //Vector3 modelSquash = new Vector3(kartModel.transform.localScale.x, kart_stats.modelBoostShrinkCurve.Evaluate(boostPercentageToFull), kartModel.transform.localScale.z);
            //kartModel.transform.localScale = Vector3.Lerp(kartModel.transform.localScale, modelSquash, boostPercentageToFull);
            boostPercentageToFullUI = boostPercentageToFull;
            yield return waitForFixedUpdateBoost;
        }
        boostPercentageToFull = 1;
        boostChargeUpCoroutineRunning = false;
    }
    #endregion
    #region Charge Down Boost Coroutine Variables and Functions

    private Coroutine boostDechargeCoroutine;
    private WaitForFixedUpdate waitForFixedUpdateDecharge = new WaitForFixedUpdate();
    private float dechargeBoostTime;
    private float boostPower;
    [Header("Boost Event")]
    [SerializeField] UnityEvent onBoostStart;
    IEnumerator DechargeBoostCoroutine()
    {
        dechargeBoostTime = 0;
        onBoostStart.Invoke();
        //kartModel.transform.localScale = new Vector3(1, 1, 1);

        while (dechargeBoostTime < kart_stats.timeBoostActive)
        {
            dechargeBoostTime += Time.deltaTime;
            float percentage = dechargeBoostTime / kart_stats.timeBoostActive;
            boostPercentageToFullUI = Mathf.Lerp(boostPercentageToFullUI, 0, percentage);
            boostPower = kart_stats.boostPowerOverTime.Evaluate(percentage) * boostPercentageToFull;
            yield return waitForFixedUpdateDecharge;
        }
        boostPercentageToFull = 0;
        boostPower = 0;
    }

    #endregion
    #endregion
    #region Move Function Variables and Functions
    private float _configureSpeed;
    public override void Move()
    {
        float button = input.Kart_Controls.ActionButton.ReadValue<float>(); //button down == 1 / button up == 0
        _configureSpeed = boostPower * kart_stats.boostMultiplier;

        if (button == 0)
        {
            currentSpeed = Mathf.Lerp(currentSpeed, kart_stats.ReturnTopSpeedStat(), Time.deltaTime * kart_stats.accelrateRate);
        }

        #region WaveEffect Logic (Commented Out)
        //waveController.WaveLength = ((currentSpeed / kart_stats.topSpeed * player_stats.topSpeed) + 1) * 4;
        #endregion
        
        //rb.AddForce(kartModel.transform.forward * ((currentSpeed + _configureSpeed) * kart_stats.forceMultiplier.DataValue));
        rb.AddForce(DriftMomentumControl() * ((currentSpeed + _configureSpeed) * kart_stats.forceMultiplier.DataValue));
    }
    #endregion
    #region Wall Check Function Variables and Functions
    public void WallCheck()
    {
        RaycastHit hit;

        Debug.DrawRay(kartNormal.transform.position, kartModel.transform.forward * wallRayDistance, Color.green);
        if (Physics.Raycast(kartNormal.transform.position, kartModel.transform.forward, out hit, wallRayDistance, groundLayer))
        {
            currentSpeed = 0;
        }
    }
    #endregion
    #region Update Charge Meter Variables and Functions
    public void UpdateChargeMeter()
    {
        _uiBoostSlider.value = boostPercentageToFullUI;
    }
    #endregion
    #region Idle Sine Wave Function Variables and Functions
    [Header("Board Jitter Settings")]
    public float upDownFrequency = 1f;
    public float maxAmplitude = 1f;
    public float minAmplitude = .1f;
    public float upDownOffset = 0f;
    private float time = 0f;
    private float speedAdjuster;
    public void BoardIdleSine()
    {
        time += Time.deltaTime;
        speedAdjuster = currentSpeed / (kart_stats.topSpeed + (player_stats.topSpeedAmount -1));
        float newAmp = Remap(speedAdjuster, 0f, 1f, minAmplitude, maxAmplitude);

        float y = newAmp * Mathf.Sin(2f * Mathf.PI * upDownFrequency * time);

        y = Mathf.Clamp(y, -maxAmplitude, maxAmplitude);

        tiltObject.transform.localPosition = new Vector3(tiltObject.transform.localPosition.x, y + maxAmplitude + upDownOffset, tiltObject.transform.localPosition.z);
    }

    public static float Remap(float value, float fromMin, float fromMax, float toMin, float toMax)
    {
        // First, normalize the input value to a [0, 1] range
        float normalizedValue = Mathf.InverseLerp(fromMin, fromMax, value);

        // Then, remap it to the desired range [toMin, toMax]
        float remappedValue = Mathf.Lerp(toMin, toMax, normalizedValue);

        return remappedValue;
    }

    #endregion
    #region Drift Variables and Functions
    private float playerInputDirectionBlendAmount = 1;
    private Vector3 driftMomentumDirection;
    private Vector3 driveMomentumDirection;
    private Vector3 moveDirection;
    [SerializeField] private AnimationCurve momentumDriveCurve;
    [Range(0,1)]
    [SerializeField] float driftThreshold;

    #region Set Drift Momentum Direction Variables and Functions
    public void SetDriftMomentumDirection()
    {
        driftMomentumDirection = rb.velocity.normalized;
    }    
    public void SetDriveMomentumDirection()
    {
        driveMomentumDirection = rb.velocity.normalized;
    }
    #endregion

    public void SpeedTurnLimiter()
    {
        float button = input.Kart_Controls.ActionButton.ReadValue<float>();
        //if (button == 0 || currentSpeed <= kart_stats.scootSpeed.DataValue) { playerInputDirectionBlendAmount = 1; return; }
        //if (currentSpeed <= kart_stats.scootSpeed.DataValue) { playerInputDirectionBlendAmount = 1; return; }

        float blendPercentage = currentSpeed / kart_stats.ReturnTopSpeedStat();
        //closer to 1 means more limiter turn
        //0 will not let the player turn / 1 will resume normal turning
        playerInputDirectionBlendAmount = kart_stats.driftTurnLimiterCurve.Evaluate(blendPercentage);
    }
    public Vector3 DriftMomentumControl()
    {
        float button = input.Kart_Controls.ActionButton.ReadValue<float>();
        //if (button == 0 || currentSpeed <= kart_stats.scootSpeed.DataValue) { return kartModel.transform.forward; }
        if (currentSpeed <= kart_stats.scootSpeed.DataValue) { return kartModel.transform.forward; }

        if(button == 0)
        {
            float blendPercentage = currentSpeed / kart_stats.ReturnTopSpeedStat(); //this might be better
            float blend = momentumDriveCurve.Evaluate(blendPercentage);

            moveDirection = (driveMomentumDirection * blend) + (kartModel.transform.forward * (1 - blend));
        }
        if (button == 1)
        {
            float blendPercentage = currentSpeed / kart_stats.ReturnTopSpeedStat(); //this might be better
            float blend = kart_stats.momentumDriftCurve.Evaluate(blendPercentage);
            moveDirection = (driftMomentumDirection * blend) + (kartModel.transform.forward * (1 - blend));
        }
        return moveDirection;
    }

    #endregion
    #region VFX Functions and Variables
    private float speedPercentage;
    public void SetBooster()
    {
        speedPercentage = currentSpeed / kart_stats.ReturnTopSpeedStat();
        boostEffect.SetFloat("BoostDuraction", speedPercentage);
    }
    private void EnableorDisableDriftEffect()
    {
        Vector2 move = input.Kart_Controls.Move.ReadValue<Vector2>();
        float button = input.Kart_Controls.ActionButton.ReadValue<float>();
        if (button == 0)
        {
            driftEffect.gameObject.SetActive(false);
            return;
        }
        if (Mathf.Abs(move.x) > driftThreshold)
        {
            driftEffect.gameObject.SetActive(true);
        }
        else
        {
            driftEffect.gameObject.SetActive(false);
            return;
        }
        if (move.x < 0)
        {
            driftEffect.SetBool("LeftorRight", true);
        }
        else if (move.x > 0)
        {
            driftEffect.SetBool("LeftorRight", false);
        }
    }
    #endregion
}
