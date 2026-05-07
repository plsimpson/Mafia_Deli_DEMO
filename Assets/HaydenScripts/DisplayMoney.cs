using TMPro;
using UnityEngine;

public class DisplayMoney : MonoBehaviour
{
    [SerializeField] TMP_Text moneyText;

    private void Update()
    {
        moneyText.text = "Moolah: $" + PlayerInventory.Money;
    }
}
