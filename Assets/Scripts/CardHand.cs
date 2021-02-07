using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardHand : MonoBehaviour
{
    private ICollection<GameObject> cards = new List<GameObject>();
    public Vector3 cardOffset = new Vector3(0.5f, 0, 0);

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
        return 0;
    }

    public bool ContainsAten()
    {
        foreach (GameObject c in cards)
        {
            if (c.GetComponent<Card>().GetValue() == 10)
            {
                return true;
            }
        }
        return false;
    }
}
