using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    private IList<GameObject> cards = new List<GameObject>();
    public Vector3 cardOffset = new Vector3(0.5f, 0, -5.0f);

    public void AddCard(GameObject card)
    {
        if (card.GetComponent<Card>() != null)
        {
            RegisterCardInHand(card);
            PositionNewCard(card);
        }
    }

    private void RegisterCardInHand(GameObject card)
    {
        cards.Add(card);
        card.GetComponent<Card>().hand = this;
    }

    private void PositionNewCard(GameObject card)
    {
        card.transform.parent = transform;
        int cardIndex = cards.Count - 1;
        card.transform.position = transform.position + (cardOffset * cardIndex);
    }

    public void ClearHand()
    {
        foreach (GameObject c in cards)
        {
            Destroy(c);
        }
        cards.Clear();
    }

    public int GetHandValue()
    {
        int sum = 0;
        foreach (GameObject c in cards)
        {
            sum += c.GetComponent<Card>().GetValue();
        }
        return sum;
    }

    /// <summary>
    /// Open up all hidden cards
    /// </summary>
    public void FlipAllCards()
    {
        foreach (GameObject c in cards)
        {
            c.GetComponent<Card>().FlipCard();
        }
    }

    /// <returns>Whether the hand contains only two cards</returns>
    public bool HandHasTwoCards()
    {
        return cards.Count == 2;
    }

    public Card GetFirstCard()
    {
        return cards[0].GetComponent<Card>();
    }
}
