using UnityEngine;
using System.Collections;

public class BaseLuck : BaseStat
{
    public BaseLuck()
    {
        StatName = "Luck";
        StatDescription = "Modifies Hero's chance at a critical hit.";
        StatType = StatTypes.LUCK;
        StatBaseValue = 0;
        StatModifiedValue = 0;
    }
}
