using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemBeh : MonoBehaviour
{
    [SerializeField] private Player_Stats player_Stats;

    public void GetStats(Player_Stats pStats)
    {
        player_Stats = pStats;
    }

    public void PickUpItem(StatType pStatType)
    {
        player_Stats.AddToStat(pStatType);
    }
}
