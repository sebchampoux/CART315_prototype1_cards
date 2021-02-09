using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : AbstractPlayer
{
    /// <summary>
    /// Point at which the dealer the dealer should stop drawing
    /// </summary>
    public int stopAt = 17;

    public override IEnumerator PlayRound()
    {
        cardHand.FlipAllCards(); // Open hidden card
        while(cardHand.GetHandValue() < stopAt)
        {
            EndTurn();
        }
        EndTurn();
        yield return null;
    }

    /// <summary>
    /// Performs the necessary operations for the dealer
    /// to check if he has a natural.  Returns whether or not
    /// it is the case.
    /// </summary>
    /// <returns>Does the dealer has a natural?</returns>
    public bool HasNatural()
    {
        Card firstCard = cardHand.GetFirstCard();
        if (firstCard.GetValue() == 10 || firstCard.GetType() == typeof(AceCard))
        {
            cardHand.FlipAllCards();
            return cardHand.GetHandValue() == BlackjackGame.BLACKJACK;
        }
        return false;
    }
}
