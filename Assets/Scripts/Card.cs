using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private int cardValue = 0;
    private bool _cardIsVisible = false;

    public void Start()
    {
        if (_cardIsVisible)
        {
            FlipCard();
        }
    }

    public virtual int GetValue()
    {
        return cardValue;
    }

    /// <summary>
    /// Flip the card from closed -> open side
    /// </summary>
    public void FlipCard()
    {
        if (!_cardIsVisible)
        {
            _cardIsVisible = true;
            transform.Rotate(0, 180, 0);
        }
    }
}
