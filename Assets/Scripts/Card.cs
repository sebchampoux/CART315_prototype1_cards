using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    [SerializeField] private int cardValue = 0;
    public CardHand hand { get; set; } = null;
    private bool cardIsHidden = true;

    public void Start()
    {
        if (!cardIsHidden)
        {
            FlipCard();
        }
    }

    /// <summary>
    /// Value of the card
    /// </summary>
    /// <returns>Value of the card</returns>
    public virtual int GetValue()
    {
        return cardValue;
    }

    /// <summary>
    /// Flip the card from back -> front side
    /// </summary>
    public void FlipCard()
    {
        transform.Rotate(0, 180, 0);
    }
}
