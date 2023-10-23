using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectItemBeh : MonoBehaviour
{
    private Player_Stats player_Stats;

    public void GetStats(Player_Stats pStats)
    {
        player_Stats = pStats;
    }

    public void PickUpItem(StatType pStatType, float pAmount)
    {
        player_Stats.AddToStat(pStatType, pAmount);
    }
}
