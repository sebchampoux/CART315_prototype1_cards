using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : AbstractPlayer
{
    private PlayerCash _playerCash;

    private float _cash = 500;
    private float _betAmount = 50;
    private float _currentBet = 0;

    public float Cash
    {
        get { return _cash; }
        private set
        {
            _cash = Mathf.Max(value, 0.0f);
            NotifyObservers();
        }
    }
    public float BetAmount
    {
        get { return _betAmount; }
        private set
        {
            _betAmount = Mathf.Max(value, 0.0f);
            NotifyObservers();
        }
    }

    public float CurrentBet
    {
        get { return _currentBet; }
        private set
        {
            _currentBet = Mathf.Max(value, 0.0f);
            NotifyObservers();
        }
    }

    private void Awake()
    {
        _playerCash = GetComponent<PlayerCash>();
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
        WinRound(winRatio);
    }

    /// <summary>
    /// Increase the current bet by defaultBetAmount
    /// </summary>
    public void Bet()
    {
        float bet = Mathf.Min(Cash, _betAmount);
        Cash -= bet;
        _currentBet += bet;
    }

    public void GetBackCurrentBet()
    {
        Cash += _currentBet;
        _currentBet = 0.0f;
    }

    public void DoubleDown()
    {
        Bet();
        DrawCard();
        EndTurn();
    }

    /// <summary>
    /// For the player, the card is always open
    /// </summary>
    /// <param name="card"></param>
    public override void AddCardToHand(Card card)
    {
        base.AddCardToHand(card);
        card.FlipCard();
    }
}
