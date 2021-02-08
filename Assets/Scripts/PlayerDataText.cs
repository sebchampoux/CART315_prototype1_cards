using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDataText : MonoBehaviour, Observer
{
    public Player player;

    public void UpdateObserver()
    {
        Text text = GetComponent<Text>();
        text.text = "Current bet: " + player.CurrentBet
            + "\nCurrent cash: " + player.Cash
            + "\nCurrent hand value: " + player.GetHandValue();
    }
}
