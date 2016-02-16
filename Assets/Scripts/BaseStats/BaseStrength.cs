using UnityEngine;
using System.Collections;

public class BaseStrength : BaseStat
{
    public BaseStrength()
    {
        StatName = "Strength";
        StatDescription = "Modifies Hero's attack power.";
        StatType = StatTypes.STRENGTH;
        StatBaseValue = 0;
        StatModifiedValue = 0;
    }
}
