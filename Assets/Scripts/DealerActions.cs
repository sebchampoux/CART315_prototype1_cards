using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DealerActions : AbstractPlayerActions
{
    /// <summary>
    /// Point at which the dealer the dealer should stop drawing
    /// </summary>
    public int stopAt = 17;

    public override IEnumerator PlayTurn()
    {
        StartCoroutine(base.PlayTurn());
        _cardHand.FlipAllCards(); // Open hidden card
        while(_cardHand.GetHandValue() < stopAt)
        {
            DrawCard();
            _cardHand.FlipAllCards();
            yield return new WaitForSeconds(0.5f);
        }
        EndTurn();
        yield return new WaitForSeconds(1.0f);
    }

    public bool FirstCardAceOrTen()
    {
        Card firstCard = _cardHand.GetFirstCard();
        return firstCard.GetValue() == 10 || firstCard.GetType() == typeof(AceCard);
    }

    /// <summary>
    /// Performs the necessary operations for the dealer
    /// to check if he has a natural.
    /// </summary>
    public void OpenAllCards()
    {
        _cardHand.FlipAllCards();
    }
}
