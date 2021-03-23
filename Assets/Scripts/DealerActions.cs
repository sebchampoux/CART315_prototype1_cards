using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerActions : AbstractPlayerActions
{
    /// <summary>
    /// Point at which the dealer the dealer should stop drawing
    /// </summary>
    public int stopAt = 17;

    public override void PlayTurn()
    {
        base.PlayTurn();
        _cardHand.FlipAllCards(); // Open hidden card
        while(_cardHand.GetHandValue() < stopAt)
        {
            DrawCard();
            _cardHand.FlipAllCards();
        }
        EndTurn();
    }

    /// <summary>
    /// Performs the necessary operations for the dealer
    /// to check if he has a natural.  Returns whether or not
    /// it is the case.
    /// </summary>
    /// <returns>Does the dealer has a natural?</returns>
    public bool HasNatural()
    {
        Card firstCard = _cardHand.GetFirstCard();
        if (firstCard.GetValue() == 10 || firstCard.GetType() == typeof(AceCard))
        {
            _cardHand.FlipAllCards();
            return _cardHand.GetHandValue() == BlackjackGame.BLACKJACK;
        }
        return false;
    }
}
