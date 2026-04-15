using UnityEngine;

public class TUTIngredientComp1 : MonoBehaviour
{
    [SerializeField] private Ingredient ingredient;

    private void OnMouseDown()
    {
        FindAnyObjectByType<Tutorial>().AddIngredient(ingredient);
    }
}