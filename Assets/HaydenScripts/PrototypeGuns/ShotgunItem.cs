using UnityEngine;

public class ShotgunItem : Item
{
    public override Equipment Equip(GameObject CharacterGO)
    {
        return CharacterGO.AddComponent<ShotgunEquipment>();
    }
}