using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesDamage : MonoBehaviour, IDoDamage
{
    [SerializeField] public Player_Stats playerStats;
    [SerializeField] public Kart_Stats kartStats;
    public float DoDamage()
    {
        return kartStats.ReturnOffenseStat();
    }

    private void Start()
    {
        GetPlayerStats();
    }

    public void GetPlayerStats()
    {
        playerStats = transform.parent.GetComponent<PlayerInfoGetter>().GetPlayerStats();
    }

    public void GetKartStats()
    {
        //kartStats = 
    }
}