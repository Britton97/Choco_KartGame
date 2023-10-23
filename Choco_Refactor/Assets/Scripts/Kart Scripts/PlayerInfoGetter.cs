using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInfoGetter : MonoBehaviour
{
    [SerializeField] public PlayerController playerController;
    [SerializeField] private Rigidbody colliderBall;
    [SerializeField] public KartController kartController;
    [SerializeField] private Player_Stats player_stats;
    [SerializeField] private UnityEvent onEnterKart;
    [SerializeField] private UnityEvent onExitKart;

    public void PlayerExitKart()
    {
        kartController = null;
        onExitKart.Invoke();
    }

    public void PlayerEnterKart()
    {
        onEnterKart.Invoke();
    }

    public Rigidbody GetColliderBall()
    {
        return colliderBall;
    }

    public Player_Stats GetPlayerStats()
    {
        return player_stats;
    }
}
