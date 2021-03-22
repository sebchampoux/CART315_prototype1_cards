using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    private IList<Card> _cards = new List<Card>();
    public Vector3 _cardsPositionOffset = new Vector3(0.5f, 0, -5.0f);

    public void AddCard(Card card)
    {
        RegisterCardInHand(card);
        PositionNewCard(card.gameObject);
    }

    private void RegisterCardInHand(Card card)
    {
        _cards.Add(card);
        card.Hand = this;
    }

    private void PositionNewCard(GameObject cardGameObject)
    {
        cardGameObject.transform.parent = transform;
        int cardIndex = _cards.Count - 1;
        cardGameObject.transform.position = transform.position + (_cardsPositionOffset * cardIndex);
    }

    public void ClearHand()
    {
        foreach (Card c in _cards)
        {
            Destroy(c);
        }
        _cards.Clear();
    }

    public int GetHandValue()
    {
        int sum = 0;
        foreach (Card c in _cards)
        {
            sum += c.GetValue();
        }
        return sum;
    }

    /// <summary>
    /// Open up all hidden cards
    /// </summary>
    public void FlipAllCards()
    {
        foreach (Card c in _cards)
        {
            c.FlipCard();
        }
    }

    /// <returns>Whether the hand contains only two cards</returns>
    public bool HandHasTwoCards()
    {
        return _cards.Count == 2;
    }

    public Card GetFirstCard()
    {
        return _cards[0];
    }
}
