using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    private readonly IList<Card> _cards = new List<Card>();
    public Vector3 _cardsPositionOffset = new Vector3(0.5f, 0, -0.5f);

    public event EventHandler CardSumChange;
    public Vector3 CardsPositionOffset
    {
        get { return _cardsPositionOffset; }
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
        PositionNewCard(card.gameObject);
        CardSumChange?.Invoke(this, EventArgs.Empty);
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
            Destroy(c.gameObject);
        }
        _cards.Clear();
        CardSumChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetNumberOfCards()
    {
        return _cards.Count;
    }

    public int GetHandValue()
    {
        int sum = 0;
        sum += SumOfRegularCards();
        sum += SumOfAces(sum);
        return sum;
    }

    private int SumOfRegularCards()
    {
        int sum = 0;
        foreach (Card c in _cards)
        {
            if (c.GetType() == typeof(AceCard))
            {
                continue;
            }
            else
            {
                sum += c.GetValue();
            }
        }
        return sum;
    }

    private int SumOfAces(int sumWithoutAces)
    {
        int sumOfAces = 0;
        int numberOfAcesInHand = NumberOfAcesInHand();
        for (int i = 0; i < numberOfAcesInHand; i++)
        {
            if (i == 0 && !WouldBustWithAn11(sumWithoutAces, numberOfAcesInHand))
            {
                sumOfAces += 11;
            }
            else
            {
                sumOfAces += 1;
            }
        }
        return sumOfAces;
    }

    /// <returns>Would counting one ace as 11, and the others as 1, go over 21?</returns>
    private static bool WouldBustWithAn11(int sumWithoutAces, int numberOfAcesInHand)
    {
        return sumWithoutAces + 11 + (numberOfAcesInHand - 1) > BlackjackGame.BLACKJACK;
    }

    private int NumberOfAcesInHand()
    {
        int numberOfAces = 0;
        foreach (Card c in _cards)
        {
            if (c.GetType() == typeof(AceCard))
            {
                numberOfAces++;
            }
        }
        return numberOfAces;
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
        CardSumChange?.Invoke(this, EventArgs.Empty);
    }

    public bool ContainsHiddenCards()
    {
        foreach (Card c in _cards)
        {
            if (!c.CardIsVisible)
            {
                return true;
            }
        }
        return false;
    }

    public Card GetFirstCard()
    {
        return _cards[0];
    }
}
