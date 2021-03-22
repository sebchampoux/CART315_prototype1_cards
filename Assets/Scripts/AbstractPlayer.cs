using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour
{
    [SerializeField] protected BlackjackGame _game;
    [SerializeField] protected CardHand _cardHand;
    protected bool _isPlaying = false;

    public event EventHandler StatusChange;

    public bool IsPlaying
    {
        get
        {
            return _isPlaying;
        }
        protected set
        {
            _isPlaying = value;
            NotifyObservers();
        }
    }

    /// <summary>
    /// Reset the player's state before starting a new round
    /// </summary>
    public virtual void ClearRound()
    {
        _cardHand.ClearHand();
        NotifyObservers();
    }

    /// <summary>
    /// Add a card to player's hand
    /// </summary>
    /// <param name="card">Card gameobject</param>
    public virtual void AddCardToHand(Card card)
    {
        _cardHand.AddCard(card);
        NotifyObservers();
    }

    /// <summary>
    /// Returns the player's hand's value
    /// </summary>
    /// <returns>hand's value</returns>
    public int GetHandValue()
    {
        return _cardHand.GetHandValue();
    }

    public void DrawCard()
    {
        _game.PlayerDrawsCard(this);
        if (_cardHand.GetHandValue() > BlackjackGame.BLACKJACK)
        {
            EndTurn();
        }
        NotifyObservers();
    }

    protected void NotifyObservers()
    {
        StatusChange?.Invoke(this, EventArgs.Empty);
    }

    public virtual void PlayTurn()
    {
        IsPlaying = true;
    }

    public void EndTurn()
    {
        IsPlaying = false;
    }
}
