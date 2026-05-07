using UnityEngine;

public class GiveStartGun : MonoBehaviour
{
    [SerializeField] private PlayerGunController playerGunController;
    [SerializeField] private Item pistolItem;
    [SerializeField] private GameObject player;

    private void Start()
    {
        if (playerGunController.CurrentEquipment == null)
        {
            playerGunController.CurrentEquipment = pistolItem.Equip(player);
        }
    }
}