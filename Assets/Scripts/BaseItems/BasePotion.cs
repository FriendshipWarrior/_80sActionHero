using UnityEngine;
using System.Collections;

public class BasePotion : BaseItem { 

    public enum PotionTypes
    {
        HEALTH,
        STAMINA,
        STRENGTH,
        INTELLIGENCE
    }

    private PotionTypes potionType;

    public PotionTypes PotionType
    {
        get;
        set;
    }
}
