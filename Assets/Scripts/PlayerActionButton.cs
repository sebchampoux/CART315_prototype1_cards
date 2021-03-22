using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionButton : MonoBehaviour
{
    public AbstractPlayer player;

    public void Start()
    {
        player.StatusChange += ToggleButtonActivation;
        ToggleButtonActivation(player, EventArgs.Empty);
    }

    private void ToggleButtonActivation(object sender, EventArgs e)
    {
        GetComponent<Button>().interactable = player.IsPlaying;
    }
}
