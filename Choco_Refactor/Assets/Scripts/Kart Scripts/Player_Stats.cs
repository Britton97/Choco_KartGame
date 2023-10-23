
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Stats", fileName = "Player Stats")]
public class Player_Stats : ScriptableObject
{
    [SerializeField] public float boost = 1;
    [SerializeField] public float topSpeed = 1;
    [SerializeField] public float turn = 1;
    [SerializeField] public float charge = 1;
    [SerializeField] public float glide = 1;
    [SerializeField] public float weight = 1;
    [SerializeField] public float offense = 1;
    [SerializeField] public float defense = 1;

    public void ResetStats()
    {
        //set all stats to 0
        boost = 1;
        topSpeed = 1;
        turn = 1;
        charge = 1;
        glide = 1;
        weight = 1;
        offense = 1;
        defense = 1;
    }


    public void AddToStat(StatType pType, float pAmount)
    {
        switch (pType)
        {
            case StatType.Boost:
                boost += pAmount;
                break;
            case StatType.TopSpeed:
                topSpeed += pAmount;
                break;
            case StatType.Turn:
                turn += pAmount;
                break;
            case StatType.Charge:
                charge += pAmount;
                break;
            case StatType.Glide:
                glide += pAmount;
                break;
            case StatType.Weight:
                weight += pAmount;
                break;
            case StatType.Offense:
                offense += pAmount;
                break;
            case StatType.Defense:
                defense += pAmount;
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