using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [Header("Sandwich Data")]
    [SerializeField] public List<Sandwich> SandwichOptions = new List<Sandwich>();
    [SerializeField] private Sandwich activeOrder;
    public List<Ingredient> beingBuilt = new List<Ingredient>();

    [Header("UI")]
    [SerializeField] TMP_Text dialogueText; // For tutorial dialogue
    [SerializeField] TMP_Text orderText;    // For sandwich orders

    [Header("Tutorial Dialogue")]
    [TextArea(3, 10)]
    public List<string> tutorialLines = new List<string>();

    private int dialogueIndex = 0;
    private bool isWaitingForInput = true; // Wait for spacebar input
    private bool orderStarted = false;     // Track if sandwich-building system is active

    private void Start()
    {
        LoadTutorialLines();
        PrintNextDialogue();
    }

    private void Update()
    {
        // Wait for spacebar input to proceed, unless the order system is active
        if (isWaitingForInput && Input.GetKeyDown(KeyCode.Space))
        {
            PrintNextDialogue();
        }
    }

    private void LoadTutorialLines()
    {
        tutorialLines = new List<string>()
        {
            // START SCREEN
            "Start Shift- Welcome to Romano’s Sandwich Shop, we are extremely delighted to add you to the family…",
            "My name’s Luca, I’m the son of Don Romano. Now listen, I know the place isn’t exactly up to code. Word on the street is our other business is doing a little too well. We need to funnel our less-than-legal funds into proper channels. That’s where you come in, make the orders perfectly, and you’ll get some nice dough.",

            // TUTORIAL SANDWICH SEQUENCE
            "Alright, this is your sandwich station.",
            "You got your meats, Ham, salami, gabagool, bacon,",
            "You also got cheese, lettuce, tomato, and onions. Since our old pal Tony was a rat, we don’t carry a lot of cheese anymore.",
            "You also got two dressings: mayo and mustard. Heard some shmuck asking for ketchup the other day. I mean, who likes that?",

            // FIRST CUSTOMER
            "Here comes your first customer.",
            "How you doing boss can I get a Ham, cheese, lettuce, and Mayo?",

            // AFTER FIRST SANDWICH
            "Great Job kid. Here comes another.",
            "Oy, I need a Bacon, Lettuce, Onion, and Mustard right now.",

            // THIRD CUSTOMER
            "Looks suitable. This guy looks like he wants a complicated one.",
            "Alright Pal, I got something to ask you…",
            "May I have a Gabagool lettuce, tomato, cheese, and mustard?",

            // FOURTH CUSTOMER
            "Excellent work, we got one more. Make sure to make this good.",
            "Well, looks like new meat into the family, I need a Salami, cheese, onion with mustard, and pronto, shrimp.",

            // END
            "Well, guess you ain't useless after all."
        };
    }

    private void PrintNextDialogue()
    {
        if (dialogueIndex < tutorialLines.Count)
        {
            // Update the dialogue TMP_Text
            if (dialogueText != null)
                dialogueText.text = tutorialLines[dialogueIndex];

            // Trigger sandwich-building system at specific dialogue lines
            if (tutorialLines[dialogueIndex] == "How you doing boss can I get a Ham, cheese, lettuce, and Mayo?" ||
                tutorialLines[dialogueIndex] == "Oy, I need a Bacon, Lettuce, Onion, and Mustard right now." ||
                tutorialLines[dialogueIndex] == "May I have a Gabagool lettuce, tomato, cheese, and mustard?" ||
                tutorialLines[dialogueIndex] == "Well, looks like new meat into the family, I need a Salami, cheese, onion with mustard, and pronto, shrimp.")
            {
               // isWaitingForInput = false; // Disable spacebar input
                orderStarted = true;      // Enable sandwich-building system
                NewOrder();
            }

            dialogueIndex++;
        }
        else
        {
            if (dialogueText != null)
                dialogueText.text = "Tutorial Complete!";
         //   isWaitingForInput = false; // Stop waiting for input after the tutorial ends
        }
    }

    private void NewOrder()
    {
        if (!orderStarted) return; // Ensure orders only start after the flag is set

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

        string s = "Order:\n";

        foreach (Ingredient ing in activeOrder.Ingredients)
            s += " - " + ing + "\n";

        // Update the order TMP_Text
        if (orderText != null)
            orderText.text = s;
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
                Debug.Log("Bad Order!");
                beingBuilt.Clear();
                return;
            }
        }

        // GOOD ORDER
        Debug.Log("Complete!");
        beingBuilt.Clear();
        SandwichOptions.RemoveAt(0);

        if (SandwichOptions.Count <= 0)
        {
            Debug.Log("Level complete");
            isWaitingForInput = true; // Re-enable spacebar input after all orders are complete
            PrintNextDialogue(); // Progress tutorial after completing the last order
        }
        else
        {
            NewOrder();
            PrintNextDialogue(); // Progress tutorial after completing each order
        }
    }
}

