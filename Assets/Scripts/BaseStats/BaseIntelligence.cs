using UnityEngine;
using System.Collections;

public class BaseIntelligence : BaseStat
{
    public BaseIntelligence()
    {
        StatName = "Intelligence";
        StatDescription = "Modifies Hero's ability to use magic.";
        StatType = StatTypes.INTELLIGENCE;
        StatBaseValue = 0;
        StatModifiedValue = 0;
    }
}
