using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand
{
    private ICollection<Card> cards = new List<Card>();

    public void AddCard(Card c)
    {
        cards.Add(c);
    }

    public void ClearHand()
    {
        cards.Clear();
    }

    public int GetHandValue()
    {
        return 0;
    }

    public bool ContainsAten()
    {
        return false;
    }
}
