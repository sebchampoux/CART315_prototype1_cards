using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour, Observable
{
    public GameObject[] observers;
    public BlackjackGame game;
    public CardHand cardHand;

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
    public void AddCardToHand(GameObject card)
    {
        card.GetComponent<Card>().FlipCard(); // All new cards are face up
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
            game.EndPlayerTurn(this);
        }
        NotifyObservers();
    }
    public void NotifyObservers()
    {
        foreach (GameObject g in observers)
        {
            if (g.GetComponent<Observer>() != null)
            {
                g.GetComponent<Observer>().Update();
            }
        }
    }

    /// <summary>
    /// Plays the player's round
    /// </summary>
    public abstract void PlayRound();
}
