using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDeck : MonoBehaviour
{
    public GameObject[] cardsDeck;
    private int currentCard = -1;

    public void Start()
    {
        ResetDeck();
    }

    /// <summary>
    /// Shuffles the card deck and places the pointer at the first card
    /// </summary>
    public void ResetDeck()
    {
        ShuffleDeck();
        currentCard = -1;
    }

    /// <summary>
    /// Shuffles the cards deck
    /// </summary>
    private void ShuffleDeck()
    {
        for (int i = cardsDeck.Length - 1; i > 1; i--)
        {
            int k = Random.Range(0, cardsDeck.Length - 1);
            GameObject toSwap = cardsDeck[k];
            cardsDeck[k] = cardsDeck[i];
            cardsDeck[i] = toSwap;
        }
    }

    /// <summary>
    /// Moves the pointer, draws and returns a copy of the current card
    /// </summary>
    /// <returns>Card prefab</returns>
    public GameObject DrawCard()
    {
        currentCard++;
        return Instantiate(cardsDeck[currentCard]);
    }

    /// <summary>
    /// Returns true if the deck is empty (all cards have been drawn)
    /// </summary>
    /// <returns>Whether all cards were drawn</returns>
    public bool DeckIsEmpty()
    {
        return currentCard >= cardsDeck.Length - 1;
    }
}
