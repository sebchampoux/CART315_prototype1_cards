using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardValue = 0;
    public CardHand hand { get; set; } = null;

    /// <summary>
    /// Value of the card
    /// </summary>
    /// <returns>Value of the card</returns>
    public virtual int GetValue()
    {
        return cardValue;
    }
}
