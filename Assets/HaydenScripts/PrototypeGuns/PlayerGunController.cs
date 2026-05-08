using UnityEngine;
using UnityEngine.UI;

public class PlayerGunController : MonoBehaviour
{

    [SerializeField] private GameObject currentWeaponUI;
    public AudioClip currentShootSound;

    public Equipment CurrentEquipment;

    [SerializeField] private Transform playerCamTrans;

    private void Start()
    {
        if (PlayerInventory.OwnedItem != null)
        {
            CurrentEquipment = PlayerInventory.OwnedItem.Equip(gameObject);
        }
    }

    private void Update()
    {
        // Buy Item
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryBuyItem();
        }

        // Add money
        /*
        if (Input.GetKeyDown(KeyCode.S))
        {
            PlayerInventory.Money += 10;
            Debug.Log("Money: $" + PlayerInventory.Money);
        }
        */

        // Use equipment
        if (Input.GetMouseButton(0))
        {
            if (CurrentEquipment != null)
            {
                CurrentEquipment.Use();
            }
        }
    }

    private void TryBuyItem()
    {
        if (Physics.Raycast(playerCamTrans.position, playerCamTrans.forward, out RaycastHit hit))
        {
            if (hit.collider.TryGetComponent<Item>(out Item hitItem))
            {
                Debug.Log("Hit item: " + hitItem.name);

                if (PlayerInventory.Money >= hitItem.Cost)
                {
                    Equipment existingEquipment = GetComponent<Equipment>();

                    GunDamage gunDamage = GetComponent<GunDamage>();

                    if (existingEquipment != null)
                    {
                        Destroy(existingEquipment);
                    }
                    if (gunDamage != null)
                    {
                        Destroy(gunDamage);
                    }

                    // Disable previous UI
                    if (currentWeaponUI != null)
                    {
                        currentWeaponUI.SetActive(false);
                    }

                    // Equip
                    CurrentEquipment = hitItem.Equip(gameObject);

                    // Enable new UI
                    currentWeaponUI = hitItem.WeaponUI;
                    currentWeaponUI.SetActive(true);
                    currentShootSound = hitItem.ShootSound;

                    Destroy(hit.collider.gameObject);

                    PlayerInventory.Money -= hitItem.Cost;
                    PlayerInventory.OwnedItem = hitItem;
                }
            }
        }
    }
}