using UnityEngine;

public class TommyItem : Item
{
    public override Equipment Equip(GameObject CharacterGO)
    {
        return CharacterGO.AddComponent<TommyEquipment>();
    }
}