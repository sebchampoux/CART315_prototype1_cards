using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Card : MonoBehaviour
{
    public int cardValue = 0;
    protected CardHand hand = null;

    public virtual int GetValue()
    {
        return 0;
    }
}
