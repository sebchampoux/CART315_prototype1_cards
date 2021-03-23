using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionButton : MonoBehaviour
{
    public AbstractPlayerActions player;

    public void Start()
    {
        player.TurnStatusChange += ToggleButtonActivation;
        ToggleButtonActivation(player, EventArgs.Empty);
    }

    private void ToggleButtonActivation(object sender, EventArgs e)
    {
        GetComponent<Button>().interactable = player.IsPlaying;
    }
}
