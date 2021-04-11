using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCash : MonoBehaviour
{
    [SerializeField] private int _currentCash = 500;
    [SerializeField] private int _defaultInitialBet = 50;
    private int _initialBet = 0;
    private int _currentBet = 0;
    [SerializeField] private AudioSource _betSound;

    public event EventHandler OnCashChange;
    public int CurrentCash
    {
        get { return _currentCash; }
        private set
        {
            _currentCash = value;
            NotifyObservers();
        }
    }

    public int InitialBet
    {
        get { return _initialBet; }
        set
        {
            _initialBet = value;
            NotifyObservers();
        }
    }

    public int CurrentBet
    {
        get { return _currentBet; }
        private set
        {
            _currentBet = value;
            NotifyObservers();
        }
    }

    protected void NotifyObservers()
    {
        OnCashChange?.Invoke(this, EventArgs.Empty);
    }

    public void ClearCurrentRound()
    {
        InitialBet = _defaultInitialBet;
        CurrentBet = 0;
    }

    public void Bet()
    {
        int betAmount = Mathf.Min(InitialBet, CurrentCash);
        CurrentBet += betAmount;
        CurrentCash -= betAmount;
        _betSound.Play();
    }

    public int LoseCurrentBet()
    {
        int lostAmount = CurrentBet;
        CurrentBet = 0;
        return lostAmount;
    }

    /// <param name="winRatio">1.0f (default value) = player won the equivalent of his initial bet</param>
    public int WinRound(float winRatio = 1.0f)
    {
        int winAmount = CurrentBet + (int)((float)CurrentBet * winRatio);
        CurrentBet = 0;
        CurrentCash += winAmount;
        return winAmount;
    }

    public int ReturnCurrentBet()
    {
        int betReturned = CurrentBet;
        CurrentCash += CurrentBet;
        CurrentBet = 0;
        return betReturned;
    }
}
