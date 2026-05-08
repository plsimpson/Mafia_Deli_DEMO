using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    public List<Sandwich> SandwichOptions = new List<Sandwich>();
    [SerializeField] private Sandwich activeOrder;
    public List<Ingredient> beingBuilt = new List<Ingredient>();
    [SerializeField] TMP_Text text;

    public float doneTime = 2f;

    [Header("Audio")]
    [SerializeField] private AudioClip[] addIngredientSfxs = new AudioClip[8];
    [SerializeField] [Range(0f, 1f)] private float sfxVolume = 1f;

    private void Start()
    {
        NewOrder();
    }

    private void NewOrder()
    {
        if (SandwichOptions.Count == 0)
        {
            Debug.Log("No more sandwiches — level complete");
            return;
        }

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
        if (beingBuilt.Contains(newIngredient)) return;

        beingBuilt.Add(newIngredient);
        Debug.Log("Added: " + newIngredient);

        // Play an add-ingredient SFX immediately when an ingredient is added
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

        if (!IsOrderComplete()) return;

        Debug.Log("Complete!");

        text.text = "Complete!\n+ $20";
        PlayerInventory.Money += 20;

        StartCoroutine(NextOrderDelay());
    }

    private bool IsOrderComplete()
    {
        if (beingBuilt.Count != activeOrder.Ingredients.Count)
            return false;

        foreach (Ingredient ing in activeOrder.Ingredients)
        {
            if (!beingBuilt.Contains(ing))
                return false;
        }

        return true;
    }

    private IEnumerator NextOrderDelay()
    {
        yield return new WaitForSeconds(doneTime);

        beingBuilt.Clear();
        SandwichOptions.RemoveAt(0);

        if (SandwichOptions.Count <= 0)
        {
            Debug.Log("Level complete");
            yield break;
        }

        NewOrder();
    }
}