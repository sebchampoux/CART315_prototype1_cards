using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame : MonoBehaviour, Observable
{
    public static readonly int BLACKJACK = 21;

    public GameObject[] observers;
    public CardDeck cardDeck;
    public Player[] players;
    public Dealer dealer;
    public bool gameIsRunning = true;
    private AbstractPlayer _currentPlayer = null;

    public AbstractPlayer CurrentPlayer
    {
        get { return _currentPlayer; }
        private set
        {
            _currentPlayer = value;
            NotifyObservers();
        }
    }

    /// <summary>
    /// Draws a card and adds it to player's hand
    /// </summary>
    /// <param name="player">Player that draws</param>
    public void PlayerDrawsCard(AbstractPlayer player)
    {
        GameObject newCard = cardDeck.DrawCard();
        player.AddCardToHand(newCard);
    }

    // Start is called before the first frame update
    void Start()
    {
        while (gameIsRunning)
        {
            cardDeck.ResetDeck();
            DistributeCards();
            TakeInitialBets();
            if (Naturals())
            {
                continue;
            }
            PlayersPlayTurns();
            Debug.Log("After player played their turn");
            //EndTurn();

            // Un seul tour pour le moment
            gameIsRunning = false;
        }
    }

    private void TakeInitialBets()
    {
        foreach (Player p in players)
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
            CurrentPlayer = p;
            StartCoroutine(p.PlayRound());
        }

        CurrentPlayer = dealer;
        StartCoroutine(dealer.PlayRound());
        
        CurrentPlayer = null;
    }

    /// <summary>
    /// Distribute wins and take losses
    /// </summary>
    private void EndTurn()
    {
        int dealersHand = dealer.GetHandValue();
        if (dealersHand > 21)
        {
            EndRoundDealerBusted();
        }
        else
        {
            EndRoundDealerDidNotBust(dealersHand);
        }
    }

    /// <summary>
    /// End the round if the dealer did not go over 21
    /// </summary>
    /// <param name="dealersHand">Value of the dealer's hand</param>
    private void EndRoundDealerDidNotBust(int dealersHand)
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

    /// <summary>
    /// End the round if the dealer went over 21
    /// </summary>
    private void EndRoundDealerBusted()
    {
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

    public void EndGame()
    {
        gameIsRunning = false;
        Application.Quit();
    }

    public void NotifyObservers()
    {
        foreach (GameObject g in observers)
        {
            Observer observer = g.GetComponent<Observer>();
            if (observer != null)
            {
                observer.UpdateObserver();
            }
        }
    }
}
