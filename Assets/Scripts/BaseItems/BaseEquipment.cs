using UnityEngine;
using System.Collections;

public class BaseEquipment : BaseItem {

	public enum EquipmentTypes
    {
        HEAD,
        BODY,
        ACCESSORY
    }

    private EquipmentTypes equipmentType;

    public EquipmentTypes EquipmenType
    {
        get;
        set;
    }
}
