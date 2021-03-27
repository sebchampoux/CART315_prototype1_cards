using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInterface : MonoBehaviour
{
    [SerializeField] private BlackjackGame _blackjackGame;
    [SerializeField] private PlayerActions _playerAction;

    [SerializeField] private Button[] _actionsButtons;
    [SerializeField] private Button _initialBetButton;
    [SerializeField] private Slider _initialBetSlider;
    [SerializeField] private Text _currentCashText;
    [SerializeField] private Text _initialBetText;
    [SerializeField] private Text _currentBetText;
    [SerializeField] private Text _roundRetroactionText;

    private void Start()
    {
        _roundRetroactionText.gameObject.SetActive(false);

        _playerAction.OnPlayStatusChange += PlayStatusChange;
        _playerAction.PlayerCash.OnCashChange += PlayerCashChange;
        _blackjackGame.TurnInfoEvent += OnBlackjackGameInfo;
        DisplayInitialValues();
    }

    private void OnBlackjackGameInfo(string message)
    {
        StartCoroutine(DisplayMessage(message));
    }

    private IEnumerator DisplayMessage(string message)
    {
        _roundRetroactionText.gameObject.SetActive(true);
        _roundRetroactionText.text = message;
        yield return new WaitForSeconds(BlackjackGame.RETROACTION_WAIT_TIME);
        _roundRetroactionText.gameObject.SetActive(false);
    }

    private void DisplayInitialValues()
    {
        PlayerCashChange(this, null);
        PlayStatusChange(this, null);
    }

    private void PlayerCashChange(object sender, System.EventArgs e)
    {
        PlayerCash pc = _playerAction.PlayerCash;
        _currentCashText.text = "$" + pc.CurrentCash;
        _initialBetSlider.maxValue = pc.CurrentCash;
        _initialBetText.text = "$" + _initialBetSlider.value.ToString();
        _initialBetSlider.value = Mathf.Min(_initialBetSlider.value, pc.CurrentCash);
        _currentBetText.text = "$" + pc.CurrentBet;
    }

    private void PlayStatusChange(object sender, System.EventArgs e)
    {
        ToggleInitialBetSlider();
        ToggleActionsButtons();
    }

    private void ToggleInitialBetSlider()
    {
        _initialBetSlider.interactable = _playerAction.WaitingForInitialBet;
        _initialBetButton.interactable = _playerAction.WaitingForInitialBet;
    }

    private void ToggleActionsButtons()
    {
        foreach (Button b in _actionsButtons)
        {
            b.interactable = _playerAction.IsPlaying;
        }
    }

    public void UpdateInitialBet()
    {
        _playerAction.PlayerCash.InitialBet = (int)_initialBetSlider.value;
    }
}
