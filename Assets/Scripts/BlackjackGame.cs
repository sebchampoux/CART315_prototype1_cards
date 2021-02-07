using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame : MonoBehaviour
{
    public static readonly int BLACKJACK = 21;
    public CardDeck cardDeck;
    public Player[] players;
    public Dealer dealer;

    /// <summary>
    /// Draws a card and adds it to player's hand
    /// </summary>
    /// <param name="player">Player that draws</param>
    public void PlayerDrawsCard(AbstractPlayer player)
    {
        GameObject newCard = cardDeck.DrawCard();
        player.AddCardToHand(newCard);
    }

    /// <summary>
    /// Ends player's turn
    /// </summary>
    /// <param name="player">Player that ends its turn</param>
    public void EndPlayerTurn(AbstractPlayer player)
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        // For each round
        // [X] Prepare deck of cards
        // [X] Distribute cards
        // [ ] Check for blackjack, if yes distribute $$
        // [ ] for each player : play turn
        // [ ] dealer plays
        // [ ] for each player not eliminated : give money
        cardDeck.ResetDeck();
        DistributeCards();
        CheckForNaturals();
    }

    /// <summary>
    /// Distribute an initial two cards to every player and the dealer
    /// </summary>
    private void DistributeCards()
    {
        for (int i = 0; i < 2; i++)
        {
            // Draw a card for each player
            GameObject card;
            foreach (Player p in players)
            {
                card = cardDeck.DrawCard();
                card.GetComponent<Card>().FlipCard();
                p.AddCardToHand(card);
            }

            // Draw a card for the dealer
            // Only make first card visible
            card = cardDeck.DrawCard();
            if (i == 0)
            {
                card.GetComponent<Card>().FlipCard();
            }
            dealer.AddCardToHand(card);
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
