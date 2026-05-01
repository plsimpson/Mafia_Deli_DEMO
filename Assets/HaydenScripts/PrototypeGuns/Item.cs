using UnityEngine;
using UnityEngine.UI;

public abstract class Item : MonoBehaviour
{
    public int Cost => cost;
    public RawImage UISprite => uiSprite;

    [SerializeField] private int cost;
    [SerializeField] private RawImage uiSprite;

    public abstract Equipment Equip(GameObject CharacterGO);
}