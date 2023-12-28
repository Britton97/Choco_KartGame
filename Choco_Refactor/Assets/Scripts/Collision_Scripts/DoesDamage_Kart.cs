using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoesDamage_Kart : DoesDamage_Abstract
{
    [SerializeField] public Kart_Stats kartStats;
    [SerializeField] public KartController kartController;

    public override float DoDamage()
    {
        return kartStats.ReturnOffenseStat();
    }

    private void Start()
    {
        kartStats = kartController.kart_stats;
    }
}
