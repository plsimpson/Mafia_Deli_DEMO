using UnityEngine;

public class IngredientComp : MonoBehaviour
{
    [SerializeField] private Ingredient ingredient;

    private void OnMouseDown()
    {
        FindAnyObjectByType<OrderController>().AddIngredient(ingredient);
    }
}