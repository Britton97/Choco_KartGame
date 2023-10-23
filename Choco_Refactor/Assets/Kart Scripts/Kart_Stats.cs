using UnityEngine;
[System.Serializable]
[CreateAssetMenu(menuName = "Kart Stats", fileName = "New Kart Stats")]
public class Kart_Stats : ScriptableObject
{
    [Header("Kart Movement Variables")]
    [SerializeField] public float topSpeed;
    [SerializeField] public float decelerateRate;
    [SerializeField] public float accelrateRate;
    [SerializeField] public DataFloat scootSpeed;
    [SerializeField] public DataFloat forceMultiplier;
    [Header("Boost Variables")]
    [SerializeField] public float boostMultiplier;
    [Tooltip("this is a function much of how much boost the player will get at the percentage of boost completion")]
    [SerializeField] public AnimationCurve boostPowerOverTime;
    [SerializeField] public float timeToChargeBoost = 1;
    [SerializeField] public float timeBoostActive = 1;
    [Header("Turn Variables")]
    [SerializeField] public float turnSpeed = 1f;
    [SerializeField] public float turnSpeedMultiplier = 1f;
    [Tooltip("X-Axis is 0 speed --> TopSpeed\r\nY-Axis is percentage of turn power. 1 = full/normal turn speed. 0 = no turn speed (You will not be able to turn)")]
    [SerializeField] public AnimationCurve turnSpeedLimiterCurve; // The curve used to adjust the rotation of the kart.
    [Header("Flight Variables")]
    [SerializeField] public float flightTime;
    [SerializeField] public AnimationCurve flightCurve;
    [SerializeField] public AnimationCurve flightPowerCurve;
    [HideInInspector] public bool canAffectCharge = true;
}
