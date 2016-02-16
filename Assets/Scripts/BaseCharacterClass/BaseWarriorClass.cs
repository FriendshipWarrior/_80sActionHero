using UnityEngine;
using System.Collections;

public class BaseWarriorClass : CharacterClass {

	public BaseWarriorClass()
    {
        CharacterClassName = "Warrior";
        CharacterClassDescription = "A fighter with skills in melee weapons";
        Stamina = 15;
        Strength = 25;
        Intelligence = 10;
    }
}
