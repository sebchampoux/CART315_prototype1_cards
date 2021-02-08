using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerActionButton : MonoBehaviour, Observer
{
    public BlackjackGame game;
    public AbstractPlayer player;

    public void Start()
    {
        ToggleButtonActivation();
    }

    public void UpdateObserver()
    {
        ToggleButtonActivation();
    }

    private void ToggleButtonActivation()
    {
        GetComponent<Button>().interactable = (game.CurrentPlayer == player);
    }
}
