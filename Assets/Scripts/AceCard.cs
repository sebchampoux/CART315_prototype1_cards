using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceCard : Card
{
    public override int GetValue()
    {
        if (hand.ContainsAten())
        {
            return 11;
        } else
        {
            return 1;
        }
    }
}
