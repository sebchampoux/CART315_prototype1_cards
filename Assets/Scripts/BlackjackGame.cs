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
    [SerializeField] private bool _gameIsRunning = true;
    private AbstractPlayerActions _currentPlayer = null;

    public event EventHandler GameUpdate;

    public AbstractPlayerActions CurrentPlayer
    {
        get { return _currentPlayer; }
        private set
        {
            _currentPlayer = value;
            NotifyObservers();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        GameLoop();
    }

    private void GameLoop()
    {
        /*
        while (_gameIsRunning)
        {
            ClearRound();
            _cardDeck.ResetDeck();
            DistributeCards();
            TakeInitialBets();
            //TODO Naturals
            PlayersPlayTurns();
            EndRound();
        }
        */
    }

    private void ClearRound()
    {
        throw new Exception("Not implemented");
    }

    private void TakeInitialBets()
    {
        throw new Exception("Not implemented");
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

    }

    /// <summary>
    /// Distribute wins and take losses
    /// </summary>
    private void EndRound()
    {

    }

    public void EndGame()
    {
        _gameIsRunning = false;
        Application.Quit();
    }

    public void NotifyObservers()
    {
        GameUpdate?.Invoke(this, EventArgs.Empty);
    }
}
