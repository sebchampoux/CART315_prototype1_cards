using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dealer : AbstractPlayer
{
    /// <summary>
    /// Point at which the dealer the dealer should stop drawing
    /// </summary>
    public int stopAt = 17;

    public override void PlayRound()
    {
        // Flip hidden card

        while(cardHand.GetHandValue() < stopAt)
        {
            game.PlayerDrawsCard(this);
        }
        game.EndPlayerTurn(this);
    }
}
