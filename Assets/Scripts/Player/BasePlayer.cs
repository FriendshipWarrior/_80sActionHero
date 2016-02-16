using UnityEngine;
using System.Collections;

public class BasePlayer {

    private string playerName;
    private int playerLevel;
    private CharacterClass playerClass;
    private int stamina;
    private int strength;
    private int intelligence;

    public string PlayerName
    {
        get;
        set;
    }
    public int PlayerLevel
    {
        get;
        set;
    }
    public CharacterClass PlayerClass
    {
        get;
        set;
    }
    public int Stamina
    {
        get;
        set;
    }
    public int Strength
    {
        get;
        set;
    }
    public int Intelligence
    {
        get;
        set;
    }
}
