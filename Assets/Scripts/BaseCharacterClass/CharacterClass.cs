using UnityEngine;
using System.Collections;

public class CharacterClass : MonoBehaviour {

    private string characterClassName;
    private string characterClassDescription;

    private int stamina;
    private int strength;
    private int intelligence;

    public string CharacterClassName
    {
        get;
        set;
    }
    public string CharacterClassDescription
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
