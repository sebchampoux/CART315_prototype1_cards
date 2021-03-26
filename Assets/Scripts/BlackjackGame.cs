using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame : MonoBehaviour
{
    public const int CARDS_ON_INITIAL_DISTRIBUTION = 2;
    public const int BLACKJACK = 21;

    [SerializeField] private CardDeck _cardDeck;
    [SerializeField] private PlayerActions[] _players;
    [SerializeField] private DealerActions _dealer;
    private AbstractPlayerActions _currentPlayer = null;
    private bool _gameIsRunning = true;

    public AbstractPlayerActions CurrentPlayer
    {
        get { return _currentPlayer; }
        private set
        {
            _currentPlayer = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (_gameIsRunning)
        {
            ClearRound();
            _cardDeck.ResetDeck();
            yield return MakeInitialBets();
            yield return DistributeCards();
            yield return CheckForNaturals();
            yield return PlayersPlayTurns();
            yield return EndRound();
        }
    }

    private void ClearRound()
    {
        foreach (PlayerActions p in _players)
        {
            p.ClearRound();
        }
        _dealer.ClearRound();
    }

    private IEnumerator MakeInitialBets()
    {
        foreach (PlayerActions p in _players)
        {
            yield return p.MakeInitialBet();
        }
    }

    /// <summary>
    /// Distribute an initial two cards to every player and the dealer
    /// </summary>
    private IEnumerator DistributeCards()
    {
        for (int i = 0; i < CARDS_ON_INITIAL_DISTRIBUTION; i++)
        {
            Card card;
            foreach (PlayerActions p in _players)
            {
                card = _cardDeck.DrawCard();
                p.AddCardToHand(card);
                yield return new WaitForSeconds(0.5f);
            }

            card = _cardDeck.DrawCard();
            if (i == 0)
            {
                card.FlipCard();
            }
            _dealer.AddCardToHand(card);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private IEnumerator CheckForNaturals()
    {
        foreach (PlayerActions player in _players)
        {
            if (!player.HasBlackjack()) continue;

            if (_dealer.FirstCardAceOrTen())
            {
                _dealer.OpenAllCards();
                if (_dealer.HasBlackjack())
                {
                    player.PlayerCash.ReturnCurrentBet();
                }
                else
                {
                    player.PlayerCash.WinRound(1.5f);
                }
                CheckOtherPlayersForNaturals(player);
                throw new NotImplementedException("Natural, faut mettre fin à la ronde!");
            }
        }
        yield return null;
    }

    private void CheckOtherPlayersForNaturals(AbstractPlayerActions currentPlayer)
    {
        foreach (PlayerActions player in _players)
        {
            if (player == currentPlayer) continue;
            if (player.HasBlackjack())
            {
                player.PlayerCash.ReturnCurrentBet();
            }
            else
            {
                player.PlayerCash.LoseCurrentBet();
            }
        }
    }

    /// <summary>
    /// Each players (including the dealer) play their turns
    /// </summary>
    private IEnumerator PlayersPlayTurns()
    {
        foreach (PlayerActions currentPlayer in _players)
        {
            CurrentPlayer = currentPlayer;
            yield return currentPlayer.PlayTurn();
            yield return new WaitForSeconds(0.5f);
            if (PlayerBusted(currentPlayer))
            {
                currentPlayer.PlayerCash.LoseCurrentBet();
            }
        }
        CurrentPlayer = _dealer;
        yield return _dealer.PlayTurn();
    }

    private static bool PlayerBusted(PlayerActions currentPlayer)
    {
        return currentPlayer.GetHandValue() > BLACKJACK;
    }

    private IEnumerator EndRound()
    {
        foreach (PlayerActions player in _players)
        {
            if (PlayerBusted(player)) continue;
            if (PlayerWins(player))
            {
                player.PlayerCash.WinRound(1.0f);
            }
            else if (player.GetHandValue() == _dealer.GetHandValue())
            {
                player.PlayerCash.ReturnCurrentBet();
            }
            else
            {
                player.PlayerCash.LoseCurrentBet();
            }
        }
        yield return new WaitForSeconds(1.0f);
    }

    private bool PlayerWins(PlayerActions player)
    {
        // Better score than the dealer, or dealer busted
        return player.GetHandValue() > _dealer.GetHandValue() || _dealer.GetHandValue() > BLACKJACK;
    }

    public void EndGame()
    {
        _gameIsRunning = false;
        Application.Quit();
    }

    public void PlayerDrawsCard(AbstractPlayerActions abstractPlayer)
    {
        Card newCard = _cardDeck.DrawCard();
        abstractPlayer.AddCardToHand(newCard);
    }
}
