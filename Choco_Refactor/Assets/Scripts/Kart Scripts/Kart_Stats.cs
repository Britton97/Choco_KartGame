using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Kart Stats", fileName = "New Kart Stats")]
public class Kart_Stats : ScriptableObject
{
    [Header("Current Player Stats (Empty During Edit)")]
    [SerializeField] private Player_Stats player_stats;

    [Header("Kart Movement Variables")]
    [SerializeField] public float topSpeed;
    [Range(0,5)]
    [Tooltip("Higher numbers make the kart slow down quicker")]
    [SerializeField] public float decelerateRate;
    [SerializeField] public float accelrateRate;
    [SerializeField] public DataFloat scootSpeed;
    [SerializeField] public DataFloat forceMultiplier;

    [Header("Boost Variables")]
    [SerializeField] public float boostMultiplier;
    [Tooltip("this is a function much of how much boost the player will get at the percentage of boost completion")]
    [SerializeField] public AnimationCurve boostPowerOverTime;
    [SerializeField] public float timeToChargeBoost = 1;
    [Tooltip("This will shrink the board model of the period charging the boost.")]
    [SerializeField] public AnimationCurve modelBoostShrinkCurve;
    [SerializeField] public float timeBoostActive = 1;

    [Header("Drift / Sliding Variables (Hover over for details)")]
    [Tooltip("X-Axis = zero speed --> top speed\r\nY-Axis = no turning allowed --> normal amount of turning")]
    [SerializeField] public AnimationCurve driftTurnLimiterCurve;
    [Tooltip("X-Axis = zero speed --> top speed\r\nY-Axis = direction going when 'A' button pushed --> direction kart is looking")]
    [SerializeField] public AnimationCurve momentumDriftCurve;

    [Header("Turn Variables")]
    [SerializeField] public float turnSpeed = 1f;
    [Tooltip("X-Axis is 0 speed --> TopSpeed\r\nY-Axis is percentage of turn power. 1 = full/normal turn speed. 0 = no turn speed (You will not be able to turn)")]
    [SerializeField] public AnimationCurve turnSpeedLimiterCurve; // The curve used to adjust the rotation of the kart.

    [Header("Flight Variables")]
    [SerializeField] public float flightTime;
    [SerializeField] public AnimationCurve flightCurve;
    [SerializeField] public AnimationCurve flightPowerCurve;
    [HideInInspector] public bool canAffectCharge = true;

    #region Stat Multiplier Variables
    const float upperModifierLimit = 5;
    [Header("Kart Specific Stat Modifiers")]
    [Range(0, upperModifierLimit)]
    [SerializeField] private float boostStatMultiplier = 1;
    [Range(0, upperModifierLimit)]
    [SerializeField] private float topSpeedStatMultiplier = 1;
    [Range(0, upperModifierLimit)]
    [SerializeField] private float turnStatMultiplier = 1;
    [Range(0, upperModifierLimit)]
    [SerializeField] private float chargeStatMultiplier = 1;
    [Range(0, upperModifierLimit)]
    [SerializeField] private float glideStatMultiplier = 1;
    [Range(0, upperModifierLimit)]
    [SerializeField] private float weightStatMultiplier = 1;
    [Range(0,upperModifierLimit)]
    [SerializeField] private float offenseStatMultiplier = 1;
    [Range(0, upperModifierLimit)]
    [SerializeField] private float defenseStatMultiplier = 1;
    #endregion
    #region Stat Return Functions
    public float ReturnBoostStat()
    {
        return boostMultiplier + (player_stats.boostAmount * boostStatMultiplier);
    }
    public float ReturnTopSpeedStat()
    {
        return topSpeed + (player_stats.topSpeedAmount * topSpeedStatMultiplier);
    }
    public float ReturnTurnStat()
    {
        return turnSpeed + (player_stats.turnAmount * turnStatMultiplier);
    }
    public float ReturnChargeStat()
    {
        return timeToChargeBoost - (player_stats.chargeAmount * chargeStatMultiplier);
    }
    public float ReturnGlideStat()
    {
        return flightTime + (player_stats.glideAmount * glideStatMultiplier);
    }
    public float ReturnWeightStat()
    {
        return decelerateRate + (player_stats.weightAmount * weightStatMultiplier);
    }
    public float ReturnOffenseStat()
    {
        return offenseStatMultiplier;
    }
    public float ReturnDefenseStat()
    {
        return defenseStatMultiplier;
    }
    #endregion
}
