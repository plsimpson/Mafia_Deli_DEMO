using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace Game.Weapons
{
    public class PlayerGunController : MonoBehaviour
    {
        public static int Money => money;

        private static int money;

        [SerializeField] private Image weaponImage;

        private Shotgun CurrentWeapon;


        private void Update()
        {
            // Buy Item
            if (Input.GetKeyDown(KeyCode.E))
            {
                TryBuyItem();
            }

            //DEBUG Add money
            if (Input.GetKeyDown(KeyCode.S))
            {
                money += 1;
                Debug.Log("Money: $" + money);
            }

            //Use weapon
            if (Input.GetMouseButtonDown(0))
            {
                if (CurrentWeapon != null)
                {
                    CurrentWeapon.Use();
                }
            }
        }

        //*
        private void TryBuyItem()
        {
            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
            {
                if (hit.collider.TryGetComponent<Item>(out Item hitItem))
                {
                    //if (money >= hitItem.Cost) // Buy item
                    {
                        //if (hitItem.ItemType == Item.ItemTypeEnum.Shotgun)
                            CurrentWeapon = transform.AddComponent<Shotgun>();
                        //Debug.Log("Purchased " + hitItem.name + " for $" + hitItem.Cost + ". New Money: " + money);
                        //weaponImage.sprite = hitItem.UISprite;
                        weaponImage.color = Color.white;
                        hit.collider.GetComponent<MeshRenderer>().enabled = false;
                        hit.collider.GetComponent<Collider>().enabled = false;
                        return;
                    }
                }
            }
            Debug.Log("Unable to buy item.");
            
        }
    }
}