using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public List<Sandwich> SandwichOptions = new List<Sandwich>();
    [SerializeField] private Sandwich activeOrder;
    public List<Ingredient> beingBuilt = new List<Ingredient>();
    [SerializeField] TMP_Text text;

    private void Start()
    {
        NewOrder();
    }

    private void NewOrder()
    {
        activeOrder = SandwichOptions[0];
        string s = "Order:\n";
        foreach (Ingredient ing in activeOrder.Ingredients)
        {
            s += " - " + ing + "\n";
        }
        text.text = s;
    }

    public void AddIngredient(Ingredient newIngredient)
    {
        // Skip ingredients already on sandwich
        if (beingBuilt.Contains(newIngredient)) return;

        beingBuilt.Add(newIngredient);
        Debug.Log("Added: " + newIngredient);

        //add sound effect here

        // Check if complete
        if (activeOrder.Ingredients.Count != beingBuilt.Count)
            return;
        for (int i = 0; i < activeOrder.Ingredients.Count; i++)
        {
            if (!beingBuilt.Contains(activeOrder.Ingredients[i]))
                return;
        }

        Debug.Log("Complete!");
        beingBuilt.Clear();
        SandwichOptions.RemoveAt(0);
        if (SandwichOptions.Count <= 0)
        {
            Debug.Log("Level complete");
        }
        else
            NewOrder();
    }
}