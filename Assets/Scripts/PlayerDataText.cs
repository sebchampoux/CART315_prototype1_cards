using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataText : MonoBehaviour, Observer
{
    public Player player;
    public Dealer dealer;
    public void UpdateObserver()
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
