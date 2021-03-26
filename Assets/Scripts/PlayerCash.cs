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

    public event EventHandler CashChange;
    public int CurrentCash
    {
        get { return _currentCash; }
        private set
        {
            _currentCash = value;
            OnChange();
        }
    }

    public int InitialBet
    {
        get { return _initialBet; }
        private set
        {
            _initialBet = value;
            OnChange();
        }
    }

    public int CurrentBet
    {
        get { return _currentBet; }
        private set
        {
            _currentBet = value;
            OnChange();
        }
    }

    protected void OnChange()
    {
        CashChange?.Invoke(this, EventArgs.Empty);
    }

    public void ClearCurrentRound()
    {
        InitialBet = _defaultInitialBet;
        CurrentBet = 0;
    }

    public void Bet()
    {
        int betAmount = Mathf.Min(InitialBet, CurrentCash);
        CurrentBet = betAmount;
        CurrentCash -= betAmount;
    }

    public void LoseCurrentBet()
    {
        CurrentBet = 0;
    }

    /// <param name="winRatio">1.0f (default value) = player won his initial bet</param>
    public void WinRound(float winRatio = 1.0f)
    {
        CurrentCash += (int)((float)CurrentBet * winRatio);
        ReturnInitialBetToCash();
    }

    private void ReturnInitialBetToCash()
    {
        CurrentCash += CurrentBet;
        CurrentBet = 0;
    }
}
