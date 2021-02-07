using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractPlayer
{
    public float cash { get; private set; } = 500;
    public float betAmount { get; private set; } = 50;
    public float currentBet { get; private set; } = 0;

    public override void ClearRound()
    {
        base.ClearRound();
        ResetCurrentBet();
        NotifyObservers();
    }

    /// <summary>
    /// Resets the player's current bet to 0
    /// </summary>
    public void ResetCurrentBet()
    {
        currentBet = 0.0f;
        NotifyObservers();
    }

    /// <summary>
    /// Player wins the round.  Gets back his current bet + some amount of winning.
    /// </summary>
    /// <param name="winRatio">How much was won (does NOT include the player's bet)</param>
    public void WinRound(float winRatio = 1.0f)
    {
        cash += currentBet * Mathf.Abs(winRatio);
        GetBackCurrentBet();
        NotifyObservers();
    }

    /// <summary>
    /// Increase the current bet by defaultBetAmount
    /// </summary>
    public void Bet()
    {
        float bet = Mathf.Min(cash, betAmount);
        cash -= bet;
        currentBet += bet;
        NotifyObservers();
    }

    public void GetBackCurrentBet()
    {
        cash += currentBet;
        currentBet = 0.0f;
        NotifyObservers();
    }

    /// <summary>
    /// End the player's turn
    /// </summary>
    public void Stay()
    {
        game.EndPlayerTurn(this);
    }

    public override void PlayRound()
    {
        Bet();
        // Coroutine for interacting with the game or wtv
    }

    public void DoubleDown()
    {
        Bet();
        DrawCard();
        game.EndPlayerTurn(this);
        NotifyObservers();
    }
}
