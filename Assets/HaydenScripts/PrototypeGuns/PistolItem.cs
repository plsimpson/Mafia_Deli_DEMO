using UnityEngine;

public class PistolItem : Item
{
    public override Equipment Equip(GameObject CharacterGO)
    {
        return CharacterGO.AddComponent<PistolEquipment>();
    }
}