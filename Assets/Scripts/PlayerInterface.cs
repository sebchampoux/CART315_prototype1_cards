using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] private PlayerActions _player;
    private PlayerCash _playerCash;

    [SerializeField] private Button[] _buttons;
    [SerializeField] private Slider _betSlider;
    [SerializeField] private Text _currentBetAmountDisplay;
}
