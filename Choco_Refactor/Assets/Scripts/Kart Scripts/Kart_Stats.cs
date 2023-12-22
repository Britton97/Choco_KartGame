using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Kart Stats", fileName = "New Kart Stats")]
public class Kart_Stats : ScriptableObject
{
    [Header("Current Player Stats (Empty During Edit)")]
    [SerializeField] private Player_Stats player_stats;

    [Header("Kart Movement Variables")]
    [SerializeField] public float baseAccelrateRate;
    [SerializeField] public DataFloat scootSpeed;
    [SerializeField] public DataFloat forceMultiplier;

    [Header("Boost Variables")]
    [Tooltip("this is a function much of how much boost the player will get at the percentage of boost completion")]
    [SerializeField] public AnimationCurve boostPowerOverTime;
    [Tooltip("This will shrink the board model of the period charging the boost.")]
    [SerializeField] public AnimationCurve modelBoostShrinkCurve;
    [SerializeField] public float baseBoostDuration = 1;

    [Header("Drift / Sliding Variables (Hover over for details)")]
    [Tooltip("X-Axis = zero speed --> top speed\r\nY-Axis = no turning allowed --> normal amount of turning")]
    [SerializeField] public AnimationCurve driftTurnLimiterCurve;
    [Tooltip("X-Axis = zero speed --> top speed\r\nY-Axis = direction going when 'A' button pushed --> direction kart is looking")]
    [SerializeField] public AnimationCurve momentumDriftCurve;

    [Header("Turn Variables")]
    [Tooltip("X-Axis is 0 speed --> TopSpeed\r\nY-Axis is percentage of turn power. 1 = full/normal turn speed. 0 = no turn speed (You will not be able to turn)")]
    [SerializeField] public AnimationCurve turnSpeedLimiterCurve; // The curve used to adjust the rotation of the kart.

    [Header("Flight Variables")]
    [SerializeField] public AnimationCurve flightCurve;
    [SerializeField] public AnimationCurve flightPowerCurve;
    [HideInInspector] public bool canAffectCharge = true;

    [Header("Base Kart Stats")]
    #region Stat Return Functions
    [SerializeField] public float baseBoost = 1;
    public float ReturnBoostStat()
    {
        return baseBoost + (player_stats.GetBoostAmount() * boostStatMultiplier);
    }

    [SerializeField] public float baseTopSpeed;
    public float ReturnTopSpeedStat()
    {
        return baseTopSpeed + (player_stats.GetTopSpeedAmount() * topSpeedStatMultiplier);
    }

    [SerializeField] public float baseTurnSpeed = 1f;
    public float ReturnTurnStat()
    {
        return baseTurnSpeed + (player_stats.GetTurnAmount() * turnStatMultiplier);
    }

    [SerializeField] public float baseChargeTime = 1;
    public float ReturnChargeStat()
    {
        return baseChargeTime - (player_stats.GetChargeAmount() * chargeStatMultiplier);
    }

    [SerializeField] public float baseGllideTime;
    public float ReturnGlideStat()
    {
        return baseGllideTime + (player_stats.GetGlideAmount() * glideStatMultiplier);
    }
    [Range(0,5)]
    [Tooltip("Higher numbers make the kart slow down quicker")]
    [SerializeField] public float baseDecelerateRate;
    public float ReturnWeightStat()
    {
        return baseDecelerateRate + (player_stats.GetWeightAmount() * weightStatMultiplier);
    }

    [SerializeField] public float baseOffenseAmount = 1;
    public float ReturnOffenseStat()
    {
        return baseOffenseAmount + (player_stats.GetOffenseAmount() * offenseStatMultiplier);
    }

    [SerializeField] public float baseDefenseAmount = 1;
    public float ReturnDefenseStat()
    {
        return baseDefenseAmount + (player_stats.GetDefenseAmount() * defenseStatMultiplier);
    }
    #endregion
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
}
