using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame : MonoBehaviour
{
    public static readonly int BLACKJACK = 21;
    public CardDeck cardDeck;
    public Player[] players;
    // Comment est-ce que j'implémente le dealer exactement?
    //public GameObject dealer;

    // Start is called before the first frame update
    void Start()
    {
        // For each round
        // Prepare deck of cards
        // Distribute cards
        // Check for blackjack, if yes distribute $$
        // for each player : play turn
        // dealer plays
        // for each player not eliminated : give money
        cardDeck.ResetDeck();
        DistributeCards();
        //CheckForNaturals();
    }

    /// <summary>
    /// Distribute an initial two cards to every player and the dealer
    /// </summary>
    private void DistributeCards()
    {
        for (int i = 0; i < 2; i++)
        {
            foreach (Player p in players)
            {
                GameObject card = cardDeck.DrawCard();
                p.AddCardToHand(card);
            }
        }
    }

    /// <summary>
    /// Checks if a player has a blackjack in his two initial cards.  If yes,
    /// take appropriate action
    /// </summary>
    private void CheckForNaturals()
    {
        foreach (Player p in players)
        {
            if (p.GetHandValue() == BLACKJACK)
            {
                // Check if the dealer has a natural too
                // If yes, tie
                // Collect the loser's bet and end the turn
            }
        }
    }
}
