using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public List<Sandwich> SandwichOptions = new List<Sandwich>();
    [SerializeField] private Sandwich activeOrder;
    public List<Ingredient> beingBuilt = new List<Ingredient>();
    [SerializeField] TMP_Text text;

    [Header("Audio")]
    [SerializeField] private AudioClip[] addIngredientSfxs = new AudioClip[8];
    [SerializeField] [Range(0f, 1f)] private float sfxVolume = 1f;

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

        // add sound effect here - pick a random clip from the 8 assigned slots
        if (addIngredientSfxs != null && addIngredientSfxs.Length > 0)
        {
            int attempts = addIngredientSfxs.Length;
            while (attempts-- > 0)
            {
                int idx = Random.Range(0, addIngredientSfxs.Length);
                var clip = addIngredientSfxs[idx];
                if (clip != null)
                {
                    AudioSource.PlayClipAtPoint(clip, transform.position, sfxVolume);
                    break;
                }
            }
        }

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