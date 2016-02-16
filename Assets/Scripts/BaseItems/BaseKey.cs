using UnityEngine;
using System.Collections;

public class BaseKey : BaseItem { 

    public enum KeyTypes
    {
        VHS
    }

    private KeyTypes keyType;

    public KeyTypes KeyType
    {
        get;
        set;
    }
}
