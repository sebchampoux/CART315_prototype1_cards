using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BlackjackGame : MonoBehaviour
{
    public const int CARDS_ON_INITIAL_DISTRIBUTION = 2;
    public const int BLACKJACK = 21;
    public const float RETROACTION_WAIT_TIME = 4.0f;
    [SerializeField] private CardDeck _cardDeck;
    [SerializeField] private PlayerActions[] _players;
    [SerializeField] private DealerActions _dealer;
    [SerializeField] private string _sceneToLoadOnEndGame;
    private AbstractPlayerActions _currentPlayer = null;
    private bool _gameIsRunning = true;
    private bool _naturals = false;

    public delegate void TurnInfoDelegate(string message);
    public event TurnInfoDelegate TurnInfoEvent;

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
            if (_naturals) continue;
            yield return PlayersPlayTurns();
            yield return EndRound();
            if (PlayersOutOfMoney())
            {
                yield return EndGame();
            }
        }
    }

    private void ClearRound()
    {
        foreach (PlayerActions p in _players)
        {
            p.ClearRound();
        }
        _dealer.ClearRound();
        _naturals = false;
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
                _naturals = true;
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
            if (PlayerBusted(currentPlayer))
            {
                int lostAmount = currentPlayer.PlayerCash.LoseCurrentBet();
                TurnInfoEvent?.Invoke(currentPlayer.PlayerName + " busted!\n-$" + lostAmount);
                yield return new WaitForSeconds(RETROACTION_WAIT_TIME);
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
                int wonAmount = player.PlayerCash.WinRound(1.0f);
                TurnInfoEvent?.Invoke(player.PlayerName + " wins!\n" + "+$" + wonAmount);
            }
            else if (player.GetHandValue() == _dealer.GetHandValue())
            {
                int returnedBet = player.PlayerCash.ReturnCurrentBet();
                TurnInfoEvent?.Invoke(player.PlayerName + " and the dealer are tied.\n+0$ - Bet returned");
            }
            else
            {
                int lostAmount = player.PlayerCash.LoseCurrentBet();
                TurnInfoEvent?.Invoke(player.PlayerName + " has less than the dealer :(\n-$" + lostAmount);
            }
            yield return new WaitForSeconds(RETROACTION_WAIT_TIME);
        }
    }

    private bool PlayerWins(PlayerActions player)
    {
        // Better score than the dealer, or dealer busted
        return player.GetHandValue() > _dealer.GetHandValue() || _dealer.GetHandValue() > BLACKJACK;
    }

    private bool PlayersOutOfMoney()
    {
        foreach (PlayerActions player in _players)
        {
            if (player.PlayerCash.CurrentCash > 0)
            {
                return false;
            }
        }
        return true;
    }

    public IEnumerator EndGame()
    {
        _gameIsRunning = false;
        TurnInfoEvent?.Invoke("All players are out of money!\nGame over!");
        yield return new WaitForSeconds(RETROACTION_WAIT_TIME);
        SceneManager.LoadSceneAsync(_sceneToLoadOnEndGame);
    }

    public void PlayerDrawsCard(AbstractPlayerActions abstractPlayer)
    {
        Card newCard = _cardDeck.DrawCard();
        abstractPlayer.AddCardToHand(newCard);
    }
}
