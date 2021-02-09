using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractPlayer
{
    private float _cash = 500;
    private float _betAmount = 50;
    private float _currentBet = 0;

    public float Cash
    {
        get { return _cash; }
        private set
        {
            _cash = value;
            NotifyObservers();
        }
    }
    public float BetAmount
    {
        get { return _betAmount; }
        private set
        {
            _betAmount = value;
            NotifyObservers();
        }
    }

    public float CurrentBet
    {
        get { return _currentBet; }
        private set
        {
            _currentBet = value;
            NotifyObservers();
        }
    }

    public override void ClearRound()
    {
        base.ClearRound();
        ResetCurrentBet();
    }

    /// <summary>
    /// Resets the player's current bet to 0
    /// </summary>
    public void ResetCurrentBet()
    {
        _currentBet = 0.0f;
    }

    /// <summary>
    /// Player wins the round.  Gets back his current bet + some amount of winning.
    /// </summary>
    /// <param name="winRatio">How much was won (does NOT include the player's bet)</param>
    public void WinRound(float winRatio = 1.0f)
    {
        _cash += _currentBet * Mathf.Abs(winRatio);
        GetBackCurrentBet();
    }

    /// <summary>
    /// Increase the current bet by defaultBetAmount
    /// </summary>
    public void Bet()
    {
        float bet = Mathf.Min(_cash, _betAmount);
        _cash -= bet;
        _currentBet += bet;
    }

    public void GetBackCurrentBet()
    {
        _cash += _currentBet;
        _currentBet = 0.0f;
    }

    public override IEnumerator PlayRound()
    {
        Debug.Log("Wait for player play");
        yield return null; // Coroutine will be stopped by EndTurn
    }

    public void DoubleDown()
    {
        Bet();
        DrawCard();
        EndTurn();
    }
}
