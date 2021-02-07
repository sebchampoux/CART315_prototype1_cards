using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataText : MonoBehaviour, Observer
{
    public Player player;

    void Observer.Update()
    {
        Text text = GetComponent<Text>();
        text.text = "Current bet: " + player.currentBet
            + "\nCurrent cash: " + player.cash
            + "\nCurrent hand value: " + player.GetHandValue();
    }
}
