using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Sandwich Data")]
    [SerializeField] private List<Sandwich> SandwichOptions = new List<Sandwich>();
    [SerializeField] private Sandwich activeOrder;
    public List<Ingredient> beingBuilt = new List<Ingredient>();

    [Header("UI")]
    [SerializeField] TMP_Text text;               // Order text
    [SerializeField] TMP_Text tutorialText;       // NEW: Tutorial dialogue text

    [Header("Tutorial Dialogue")]
    [TextArea(3, 10)]
    public List<string> tutorialLines = new List<string>();

    private int dialogueIndex = 0;

    private void Start()
    {
        LoadTutorialLines();
        PrintNextDialogue();
        NewOrder();
    }

    private void LoadTutorialLines()
    {
        tutorialLines = new List<string>()
        {
            // START SCREEN
            "Start Shift – Welcome to Romano’s Sandwich Shop. We’re delighted to add you to the family.",
            "My name’s Luca, son of Don Romano. The place ain’t exactly up to code… but that’s why you’re here.",
            "Make the orders perfectly and you’ll get some nice dough.",

            // TUTORIAL SANDWICH SEQUENCE
            "Alright, this is your sandwich station.",
            "You got your meats: ham, salami, gabagool. Cheeses: American, provolone, Pepper Jack. Every sandwich today is on a sub roll.",
            "Over here are your toppings: lettuce, tomato, onions.",
            "And your dressings: oil, vinegar, rosemary, honey mustard.",

            // FIRST CUSTOMER
            "Here comes your first customer now.",
            "Customer: 'How ya doin boss? Can I get a gabagool, American, oil, vinegar, and rosemary?'",

            // AFTER FIRST SANDWICH
            "Good order: 'Give the Don my thanks.'",
            "Bad order: 'Are you kidding me? What kind of shop are you running here?!'",

            // SECOND CUSTOMER
            "Here comes another one now.",
            "Customer: 'Woah pal, you got something I want. Ham, salami, provolone, honey mustard. On the hop, buddy boy.'",
            "Luca: 'This schmuck wants it fast. Finish before the timer for max profits.'",

            // AFTER SECOND SANDWICH
            "Good order: 'Good job, you’re not sleeping with the fishes tonight.'",
            "Bad order: 'You think this is okay? I’ll show you.'",

            // FIGHT SEQUENCE
            "A customer enters…",
            "'Hey there, I was curious if I could get a… KNUCKLE SANDWICH!'",
            "This guy’s trying to whack you. Take him out with whatever you got nearby.",
            "More guys are funneling in. Take 'em out.",

            // POST ROUND
            "Shift Complete. New weapons unlocked."
        };
    }

    private void PrintNextDialogue()
    {
        if (dialogueIndex < tutorialLines.Count)
        {
            if (tutorialText != null)
                tutorialText.text = tutorialLines[dialogueIndex];

            dialogueIndex++;
        }
    }

    private void NewOrder()
    {
        if (SandwichOptions == null || SandwichOptions.Count == 0)
        {
            Debug.LogError("SandwichOptions is empty or null!");
            return;
        }

        activeOrder = SandwichOptions[0];

        if (activeOrder == null || activeOrder.Ingredients == null)
        {
            Debug.LogError("Active order or its ingredients are null!");
            return;
        }

        if (text == null)
        {
            Debug.LogError("TMP_Text component is not assigned!");
            return;
        }

        string s = "Order:\n";

        foreach (Ingredient ing in activeOrder.Ingredients)
            s += " - " + ing + "\n";

        text.text = s;
    }

    public void AddIngredient(Ingredient newIngredient)
    {
        if (beingBuilt.Contains(newIngredient)) return;

        beingBuilt.Add(newIngredient);

        // Not complete yet
        if (activeOrder.Ingredients.Count != beingBuilt.Count)
            return;

        // Check correctness
        for (int i = 0; i < activeOrder.Ingredients.Count; i++)
        {
            if (!beingBuilt.Contains(activeOrder.Ingredients[i]))
            {
                PrintNextDialogue(); // bad order line
                beingBuilt.Clear();
                return;
            }
        }

        // GOOD ORDER
        PrintNextDialogue(); // good order line

        beingBuilt.Clear();
        SandwichOptions.RemoveAt(0);

        if (SandwichOptions.Count <= 0)
        {
            PrintNextDialogue(); // final line
        }
        else
        {
            NewOrder();
        }
    }
}

