using UnityEngine;
using System.Collections;

[System.Serializable]
public class BaseStamina : BaseStat { 
    public BaseStamina()
    {
        StatName = "Stamina";
        StatDescription = "Directly modifies player's health.";
        StatType = StatTypes.STAMINA;
        StatBaseValue = 0;
        StatModifiedValue = 0;
    }
}
