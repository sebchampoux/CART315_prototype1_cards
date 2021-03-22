using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AceCard : Card
{
    public override int GetValue()
    {
        // Not a very sophisticated implementation but will do for now
        return Hand.HandHasTwoCards() ? 11 : 1;
    }
}
