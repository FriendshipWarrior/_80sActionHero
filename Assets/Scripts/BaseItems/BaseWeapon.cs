using UnityEngine;
using System.Collections;

public class BaseWeapon : BaseItem {

	public enum WeaponTypes
    {
        SWORD,
        GUN
    }
    private WeaponTypes weaponType;
    
    public WeaponTypes WeaponType{get;set;}
}
