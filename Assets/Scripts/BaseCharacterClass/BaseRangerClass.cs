using UnityEngine;
using System.Collections;

public class BaseRangerClass : CharacterClass {

    public BaseRangerClass()
    {
        CharacterClassName = "Ranger";
        CharacterClassDescription = "A fighter with skills in ranged weapons";
        Stamina = 25;
        Strength = 20;
        Intelligence = 15;
    }
}
