
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats", fileName = "Player Stats")]
public class Player_Stats : ScriptableObject
{
    [Header("Current Player Stats")]
    [SerializeField] public float boostAmount = 0;
    [SerializeField] public float topSpeedAmount = 0;
    [SerializeField] public float turnAmount = 0;
    [SerializeField] public float chargeAmount = 0;
    [SerializeField] public float glideAmount = 0;
    [SerializeField] public float weightAmount = 0;
    [SerializeField] public float offenseAmount = 0;
    [SerializeField] public float defenseAmount = 0;
    [Header("")]
    [SerializeField] private bool doNotAllowStatReset = false;

    public void ResetStats()
    {
        if(doNotAllowStatReset) return;

        boostAmount = 0;
        topSpeedAmount = 0;
        turnAmount = 0;
        chargeAmount = 0;
        glideAmount = 0;
        weightAmount = 0;
        offenseAmount = 0;
        defenseAmount = 0;
    }


    public void AddToStat(StatType pType)
    {
        switch (pType)
        {
            case StatType.Boost:
                boostAmount += 1;
                break;
            case StatType.TopSpeed:
                topSpeedAmount += 1;
                break;
            case StatType.Turn:
                turnAmount += 1;
                break;
            case StatType.Charge:
                chargeAmount += 1;
                break;
            case StatType.Glide:
                glideAmount += 1;
                break;
            case StatType.Weight:
                weightAmount += 1;
                break;
            case StatType.Offense:
                offenseAmount += 1;
                break;
            case StatType.Defense:
                defenseAmount += 1;
                break;
        }
    }
}

public enum StatType
{
    Boost,
    TopSpeed,
    Turn,
    Charge,
    Glide,
    Weight,
    Offense,
    Defense
}