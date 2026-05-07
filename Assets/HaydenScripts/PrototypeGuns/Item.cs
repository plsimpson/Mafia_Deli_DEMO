using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public int Cost => cost;

    [SerializeField] private int cost;

    [SerializeField] private GameObject weaponUI;
    public GameObject WeaponUI => weaponUI;

    [SerializeField] private AudioClip shootSound;

    public AudioClip ShootSound => shootSound;

    public abstract Equipment Equip(GameObject CharacterGO);
}