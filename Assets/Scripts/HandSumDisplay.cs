using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HandSumDisplay : MonoBehaviour
{
    [SerializeField] private CardHand _cardHand;
    private TextMeshPro _sumDisplayText;
    private Vector3 _initialPosition;

    void Start()
    {
        _cardHand.CardSumChange += UpdateDisplayValue;
        _sumDisplayText = GetComponent<TextMeshPro>();
        _initialPosition = transform.position;
    }
    
    private void UpdateDisplayValue(object sender, System.EventArgs e)
    {
        ToggleDisplayVisibility();
        _sumDisplayText.text = _cardHand.GetHandValue().ToString();
        RepositionDisplay();
    }
    
    private void ToggleDisplayVisibility()
    {
        if (_cardHand.GetNumberOfCards() == 0 || _cardHand.ContainsHiddenCards())
        {
            gameObject.SetActive(false);
        }
        else if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
    }

    private void RepositionDisplay()
    {
        int nbrOfCards = _cardHand.GetNumberOfCards();
        int initialNbrOfCards = 2;
        int nbrOfCardsOffset = Mathf.Max(0, nbrOfCards - initialNbrOfCards);
        Vector3 cardsOffset = _cardHand.CardsPositionOffset;
        transform.position = _initialPosition + nbrOfCardsOffset * cardsOffset;
    }
}
