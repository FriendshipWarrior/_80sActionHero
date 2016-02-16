using UnityEngine;
using System.Collections;

public class BaseMageClass : CharacterClass {

    public BaseMageClass()
    {
        CharacterClassName = "Mage";
        CharacterClassDescription = "A fighter with magical powers";
        Stamina = 15;
        Strength = 10;
        Intelligence = 25;
    }
}
