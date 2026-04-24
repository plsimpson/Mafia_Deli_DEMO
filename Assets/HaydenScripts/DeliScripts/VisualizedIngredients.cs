using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class VisualizedIngredients : MonoBehaviour
{
    [SerializeField] private GameObject Ham;
    [SerializeField] private GameObject Salami;
    [SerializeField] private GameObject Gabagool;
    [SerializeField] private GameObject Bacon;
    [SerializeField] private GameObject Cheese;
    [SerializeField] private GameObject Lettuce;
    [SerializeField] private GameObject Tomato;
    [SerializeField] private GameObject Onion;
    [SerializeField] private GameObject Mustard;
    [SerializeField] private GameObject Mayo;

    private Dictionary<Ingredient, GameObject> ingredientMap;

    [SerializeField] private OrderController OrderController;

    void Start()
    {
        ingredientMap = new Dictionary<Ingredient, GameObject>()
        {
            { Ingredient.Ham, Ham },
            { Ingredient.Salami, Salami },
            { Ingredient.Gabagool, Gabagool },
            { Ingredient.Bacon, Bacon },
            { Ingredient.Cheese, Cheese },
            { Ingredient.Lettuce, Lettuce },
            { Ingredient.Tomato, Tomato },
            { Ingredient.Onion, Onion },
            { Ingredient.Mustard, Mustard },
            { Ingredient.Mayo, Mayo }
        };

        // Turn everything off initially
        foreach (var item in ingredientMap.Values)
        {
            item.SetActive(false);
        }
    }

    void Update()
    {
        foreach (var pair in ingredientMap)
        {
            pair.Value.SetActive(OrderController.beingBuilt.Contains(pair.Key));
        }
    }
}