using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataText : MonoBehaviour
{
    public Player player;
    public Dealer dealer;

    private void Start()
    {
        player.StatusChange += UpdatePlayerDataText;
        UpdatePlayerDataText(player, EventArgs.Empty);
    }

    public void UpdatePlayerDataText(object sender, EventArgs e)
    {
        int playerHand = player.GetHandValue();
        int dealerHand = dealer.GetHandValue();
     
        Text text = GetComponent<Text>();
        text.text = "Current bet: " + player.CurrentBet
            + "\nCurrent cash: " + player.Cash
            + "\nCurrent hand value: " + playerHand
            + "\nCurrent dealer hand value: " + dealerHand;
    }
}
