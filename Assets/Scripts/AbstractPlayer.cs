using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour, Observable
{
    [SerializeField] protected GameObject[] observers;
    [SerializeField] protected BlackjackGame game;
    [SerializeField] protected CardHand cardHand;
    protected bool _isPlaying = false;

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
        cardHand.ClearHand();
        NotifyObservers();
    }

    /// <summary>
    /// Add a card to player's hand
    /// </summary>
    /// <param name="card">Card gameobject</param>
    public virtual void AddCardToHand(GameObject card)
    {
        cardHand.AddCard(card);
        NotifyObservers();
    }

    /// <summary>
    /// Returns the player's hand's value
    /// </summary>
    /// <returns>hand's value</returns>
    public int GetHandValue()
    {
        return cardHand.GetHandValue();
    }

    public void DrawCard()
    {
        game.PlayerDrawsCard(this);
        if (cardHand.GetHandValue() > BlackjackGame.BLACKJACK)
        {
            EndTurn();
        }
        NotifyObservers();
    }

    public void NotifyObservers()
    {
        foreach (GameObject g in observers)
        {
            if (g.GetComponent<Observer>() != null)
            {
                g.GetComponent<Observer>().UpdateObserver();
            }
        }
    }

    /// <summary>
    /// Starts the player's turn
    /// </summary>
    public virtual void PlayTurn()
    {
        IsPlaying = true;
    }

    public void EndTurn()
    {
        IsPlaying = false;
    }
}
