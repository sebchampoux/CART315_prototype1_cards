using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackjackGame : MonoBehaviour
{
    public static readonly int BLACKJACK = 21;

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
        GameLoop();
    }

    private void GameLoop()
    {
        //while (_gameIsRunning)
        //{
            ClearRound();
            _cardDeck.ResetDeck();
            MakeInitialBets();
            DistributeCards();
            //TODO Naturals
            PlayersPlayTurns();
            EndRound();
        //}
    }

    private void ClearRound()
    {
        foreach(PlayerActions p in _players)
        {
            p.ClearRound();
        }
        _dealer.ClearRound();
    }

    private void MakeInitialBets()
    {
        foreach(PlayerActions p in _players)
        {
            StartCoroutine(p.MakeInitialBet());
        }
    }

    public void PlayerDrawsCard(AbstractPlayerActions abstractPlayer)
    {
        Card newCard = _cardDeck.DrawCard();
        abstractPlayer.AddCardToHand(newCard);
    }

    /// <summary>
    /// Distribute an initial two cards to every player and the dealer
    /// </summary>
    private void DistributeCards()
    {
        for (int i = 0; i < 2; i++)
        {
            Card card;
            foreach (PlayerActions p in _players)
            {
                card = _cardDeck.DrawCard();
                p.AddCardToHand(card);
            }

            card = _cardDeck.DrawCard();
            if (i == 0)
            {
                card.FlipCard();
            }
            _dealer.AddCardToHand(card);
        }
    }

    /// <summary>
    /// Each players (including the dealer) play their turns
    /// </summary>
    private void PlayersPlayTurns()
    {
        foreach(PlayerActions currentPlayer in _players)
        {
            CurrentPlayer = currentPlayer;
            StartCoroutine(currentPlayer.PlayTurn());
        }
        CurrentPlayer = _dealer;
        _dealer.PlayTurn();
    }

    /// <summary>
    /// Distribute wins and take losses
    /// </summary>
    private void EndRound()
    {
        Debug.Log("End round");
    }

    public void EndGame()
    {
        _gameIsRunning = false;
        Application.Quit();
    }
}
