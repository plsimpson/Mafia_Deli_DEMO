using UnityEngine;
using UnityEngine.UI;

public class PlayerGunController : MonoBehaviour
{
    public static int Money => money;
    private static int money;

    [SerializeField] private RawImage weaponImage;
    private Equipment CurrentEquipment;

    [SerializeField] private Transform playerCamTrans;

    private void Update()
    {
        // Buy Item
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryBuyItem();
        }

        // Add money
        if (Input.GetKeyDown(KeyCode.S))
        {
            money += 10;
            Debug.Log("Money: $" + money);
        }

        // Use equipment
        if (Input.GetMouseButtonDown(0))
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
                if (money >= hitItem.Cost) // Buy item
                {
                    weaponImage.texture = hitItem.UISprite.texture;
                    CurrentEquipment = hitItem.Equip(gameObject); // Stores the Equipment component returned by this function
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}