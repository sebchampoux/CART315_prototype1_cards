using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActions : AbstractPlayerActions
{
    private PlayerCash _playerCash;
    private bool _waitingForInitialBet = false;

    public bool WaitingForInitialBet
    {
        get { return _waitingForInitialBet; }
        set
        {
            _waitingForInitialBet = value;
            NotifyObservers();
        }
    }

    public PlayerCash PlayerCash
    {
        get { return _playerCash; }
    }

    private void Awake()
    {
        _playerCash = GetComponent<PlayerCash>();
    }

    public override void ClearRound()
    {
        base.ClearRound();
        _playerCash.ClearCurrentRound();
    }

    /// <summary>
    /// For the player, the card is always open
    /// </summary>
    /// <param name="card"></param>
    public override void AddCardToHand(Card card)
    {
        base.AddCardToHand(card);
        card.FlipCard();
    }

    public override IEnumerator PlayTurn()
    {
        StartCoroutine(base.PlayTurn());
        yield return new WaitWhile(() => IsPlaying);
    }

    public void DoubleDown()
    {
        _playerCash.Bet();
        DrawCard();
        EndTurn();
    }

    public IEnumerator MakeInitialBet()
    {
        WaitingForInitialBet = true;
        yield return new WaitWhile(() => WaitingForInitialBet);
    }

    public void ConfirmInitialBet()
    {
        _playerCash.Bet();
        WaitingForInitialBet = false;
    }
}
