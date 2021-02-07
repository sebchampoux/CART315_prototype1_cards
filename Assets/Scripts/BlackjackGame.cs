using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame : MonoBehaviour
{
    public static readonly int BLACKJACK = 21;
    public CardDeck cardDeck;
    public Player[] players;
    public Dealer dealer;
    public bool gameIsRunning = true;

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
        // [X] Check for blackjack, if yes distribute $$
        // [ ] for each player : play turn
        // [X] dealer plays
        // [X] for each player not eliminated : give money
        while (gameIsRunning)
        {

            cardDeck.ResetDeck();
            DistributeCards();
            TakeInitialBets();
            
            // Temporaire
            break;

            if (Naturals())
            {
                continue;
            }
            PlayersPlayTurns();
            EndTurn();

            // Un seul tour pour le moment
            gameIsRunning = false;
        }
    }

    private void TakeInitialBets()
    {
        foreach( Player p in players)
        {
            p.Bet();
        }
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
    /// <returns>Whether some naturals are found</returns>
    private bool Naturals()
    {
        // Filter players according to who has a natural and who doesn't
        IList<Player> playersWithNaturals = new List<Player>();
        IList<Player> playersWithoutNaturals = new List<Player>();
        foreach (Player p in players)
        {
            if (p.GetHandValue() == BLACKJACK)
            {
                playersWithNaturals.Add(p);
            }
            else
            {
                playersWithoutNaturals.Add(p);
            }
        }

        // If some players have a natural, distribute money if possible
        // and end the round
        if (playersWithNaturals.Count > 0)
        {
            if (dealer.HasNatural())
            {
                foreach (Player p in playersWithNaturals)
                {
                    p.GetBackCurrentBet();
                }
                foreach (Player p in playersWithoutNaturals)
                {
                    p.ResetCurrentBet();
                }
            }
            else
            {
                foreach (Player p in playersWithNaturals)
                {
                    p.WinRound(1.5f);
                }
            }
            return true;
        }
        return false;
    }

    /// <summary>
    /// Each players (including the dealer) play their turns
    /// </summary>
    private void PlayersPlayTurns()
    {
        foreach (Player p in players)
        {
            p.PlayRound();
        }
        dealer.PlayRound();
    }

    /// <summary>
    /// Distribute wins and take losses
    /// </summary>
    private void EndTurn()
    {
        int dealersHand = dealer.GetHandValue();
        if (dealersHand > 21)
        {
            // Dealer busted
            foreach (Player p in players)
            {
                if (p.GetHandValue() <= BLACKJACK)
                {
                    p.WinRound();
                }
                else
                {
                    p.ResetCurrentBet();
                }
            }
        }
        else
        {
            foreach (Player p in players)
            {
                int playersHand = p.GetHandValue();
                if (playersHand > BLACKJACK || playersHand < dealersHand)
                {
                    p.ResetCurrentBet();
                }
                else if (playersHand == dealersHand)
                {
                    p.GetBackCurrentBet();
                }
                else
                {
                    p.WinRound();
                }
            }
        }
    }

    public void EndGame()
    {
        gameIsRunning = false;
        Application.Quit();
    }
}
