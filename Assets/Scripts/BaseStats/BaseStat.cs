using UnityEngine;
using System.Collections;

public class BaseStat {
    private string _name;
    private string _description;
    private int _baseValue;
    private int _modifiedValue;
    private StatTypes _type;

    public enum StatTypes
    {
        STAMINA,
        STRENGTH,
        INTELLIGENCE,
        LUCK
    }

    public string StatName { get; set; }
    public string StatDescription { get; set; }
    public int StatBaseValue { get; set; }
    public int StatModifiedValue { get; set; }
    public StatTypes StatType { get; set; } 

}
