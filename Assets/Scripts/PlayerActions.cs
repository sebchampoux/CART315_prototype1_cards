using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : AbstractPlayerActions
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
