using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCash : MonoBehaviour
{
    [SerializeField] private uint _currentCash = 500;
    [SerializeField] private uint _defaultBet = 50;
    private uint _currentBet = 0;

    public uint CurrentCash
    {
        get { return _currentCash; }
        private set
        {
            _currentCash = value;
            OnChange();
        }
    }

    public uint CurrentBet
    {
        get { return _currentBet; }
        private set
        {
            _currentBet = value;
            OnChange();
        }
    }

    public event EventHandler CashChange;

    protected void OnChange()
    {
        CashChange?.Invoke(this, EventArgs.Empty);
    }

    public void LoseCurrentBet()
    {
        CurrentBet = 0;
    }

    /// <param name="winRatio">1.0f (default value) = player won his initial bet</param>
    public void WinRound(float winRatio = 1.0f)
    {
        CurrentCash += (uint)((float)CurrentBet * winRatio);
        ReturnInitialBetToCash();
    }

    private void ReturnInitialBetToCash()
    {
        CurrentCash += CurrentBet;
        CurrentBet = 0;
    }
}
