using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/Item", fileName = "Item")]
public class DataItem : ScriptableObject
{
    [SerializeField] StatType statType;
    [SerializeField] GameObject boost;
    [SerializeField] GameObject topSpeed;
    [SerializeField] GameObject turn;
    [SerializeField] GameObject charge;
    [SerializeField] GameObject glide;
    [SerializeField] GameObject weight;
    [SerializeField] GameObject offense;
    [SerializeField] GameObject defense;
    [SerializeField] GameObject questionMark;

    public GameObject ChooseItemPrefab(StatType pType)
    {
        //return the correct prefab based on the stat type
        switch (pType)
        {
            case StatType.Boost:
                return boost;
            case StatType.TopSpeed:
                return topSpeed;
            case StatType.Turn:
                return turn;
            case StatType.Charge:
                return charge;
            case StatType.Glide:
                return glide;
            case StatType.Weight:
                return weight;
            case StatType.Offense:
                return offense;
            case StatType.Defense:
                return defense;
            default:
                return questionMark;
        }
    }
    public StatType ReturnRandomType()
    {
        //return a random stat type
        int random = Random.Range(0, 8);
        switch (random)
        {
            case 0:
                return StatType.Boost;
            case 1:
                return StatType.TopSpeed;
            case 2:
                return StatType.Turn;
            case 3:
                return StatType.Charge;
            case 4:
                return StatType.Glide;
            case 5:
                return StatType.Weight;
            case 6:
                return StatType.Offense;
            case 7:
                return StatType.Defense;
            default:
                return StatType.Boost;
        }
    }
}


/*
 * public enum StatType
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
 */
