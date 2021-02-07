using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractPlayer
{
    [SerializeField] private int cash = 500;
    [SerializeField] private int betAmount = 50;
    [HideInInspector] public int currentBet { get; private set; } = 0;

    /// <summary>
    /// Resets the player's current bet to 0
    /// </summary>
    public void ResetCurrentBet()
    {
        currentBet = 0;
    }

    /// <summary>
    /// Increase the current bet by defaultBetAmount
    /// </summary>
    public void Bet()
    {
        int initialBet = Mathf.Min(cash, betAmount);
        cash -= initialBet;
        currentBet += initialBet;
    }

    /// <summary>
    /// End the player's turn
    /// </summary>
    public void Stay()
    {

    }

    public override void PlayRound()
    {
        ResetCurrentBet();
        Bet();
    }
}
