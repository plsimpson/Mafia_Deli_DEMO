using UnityEngine;

namespace Game.Weapons
{
    public class GunItem : MonoBehaviour
    {
        public int Cost => cost;
        public Sprite UISprite => uiSprite;
        public ItemTypeEnum ItemType => itemType;

        public enum ItemTypeEnum { Shotgun }

        [SerializeField] protected int cost;
        [SerializeField] protected Sprite uiSprite;
        [SerializeField] private ItemTypeEnum itemType;
    }
}