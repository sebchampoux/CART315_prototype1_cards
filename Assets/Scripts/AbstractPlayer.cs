using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractPlayer : MonoBehaviour
{
    public BlackjackGame game;
    public CardHand cardHand;

    /// <summary>
    /// Reset the player's state before starting a new round
    /// </summary>
    public void ClearRound()
    {
        cardHand.ClearHand();
    }

    /// <summary>
    /// Add a card to player's hand
    /// </summary>
    /// <param name="card">Card gameobject</param>
    public void AddCardToHand(GameObject card)
    {
        cardHand.AddCard(card);
    }

    /// <summary>
    /// Returns the player's hand's value
    /// </summary>
    /// <returns>hand's value</returns>
    public int GetHandValue()
    {
        return cardHand.GetHandValue();
    }

    protected void DrawCard()
    {
        game.PlayerDrawsCard(this);
    }

    /// <summary>
    /// Plays the player's round
    /// </summary>
    public abstract void PlayRound();
}
