using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tutorial : DemoSceneLoader
{
    [Header("Sandwich Data")]
    [SerializeField] public List<Sandwich> SandwichOptions = new List<Sandwich>();
    [SerializeField] private Sandwich activeOrder;
    public List<Ingredient> beingBuilt = new List<Ingredient>();

    [Header("UI")]
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text orderText;

    [Header("Tutorial Dialogue")]
    [TextArea(3, 10)]
    public List<string> tutorialLines = new List<string>();

    private int dialogueIndex = 0;
    private bool isWaitingForInput = true;
    private bool orderStarted = false;

    private float doneTime = 2f;

    private void Start()
    {
        LoadTutorialLines();
        PrintNextDialogue();
    }

    private void Update()
    {
        if (isWaitingForInput && Input.GetKeyDown(KeyCode.Space))
        {
            PrintNextDialogue();
        }
    }

    private void LoadTutorialLines()
    {
        tutorialLines = new List<string>()
        {
            "Welcome to Tony Romano’s Sandwich Shop, we are extremely delighted to add you to the family… " +
            "Press Space to Continue.",
            "My name’s Luca, I’m the son of Don Romano. Now listen, I know the place isn’t exactly up to code. Word on the street is our other business is doing a little too well. We need to funnel our less-than-legal funds into proper channels. That’s where you come in, make the orders perfectly, and you’ll get some nice dough.",

            "Alright, this is your sandwich station.",
            "You got your meats, Ham, salami, gabagool, bacon,",
            "You also got cheese, lettuce, tomato, and onions. Since our old pal Tony was a rat, we don’t carry a lot of cheese anymore.",
            "You also got two dressings: mayo and mustard. Heard some shmuck asking for ketchup the other day. I mean, who likes that?",

            "Here comes your first customer.",
            "How you doing boss can I get a Ham, cheese, lettuce, and Mayo?",

            "Great Job kid. Here comes another.",
            "Oy, I need a Bacon, Lettuce, Onion, and Mustard right now.",

            "Looks suitable. This guy looks like he wants a complicated one.",
            "Alright Pal, I got something to ask you…",
            "May I have a Gabagool lettuce, tomato, cheese, and mustard?",

            "Excellent work, we got one more. Make sure to make this good.",
            "Well, looks like new meat into the family, I need a Salami, cheese, onion with mustard, and pronto, shrimp.",

            "Uh oh. Looks like some schmucks are trying to crash the party. Teach em a lesson."
        };
    }

    private void PrintNextDialogue()
    {
        if (dialogueIndex < tutorialLines.Count)
        {
            string line = tutorialLines[dialogueIndex];

            if (dialogueText != null)
                dialogueText.text = line;

            // Disable spacebar ONLY on order lines
            switch (line)
            {
                case "How you doing boss can I get a Ham, cheese, lettuce, and Mayo?":
                case "Oy, I need a Bacon, Lettuce, Onion, and Mustard right now.":
                case "May I have a Gabagool lettuce, tomato, cheese, and mustard?":
                case "Well, looks like new meat into the family, I need a Salami, cheese, onion with mustard, and pronto, shrimp.":
                    StartOrder();
                    isWaitingForInput = false; // disable spacebar
                    break;
            }

            dialogueIndex++;
        }
        else
        {
            if (dialogueText != null)
                dialogueText.text = "Uh oh. Looks like some schmucks are trying to crash the party. Teach em a lesson.";

            SceneLoader();
        }
    }

    private void StartOrder()
    {
        if (SandwichOptions == null || SandwichOptions.Count == 0)
        {
            Debug.LogError("No more sandwich options available.");
            return;
        }

        orderStarted = true;
        activeOrder = SandwichOptions[0];
        NewOrder();
    }

    private void NewOrder()
    {
        if (!orderStarted) return;

        if (activeOrder == null || activeOrder.Ingredients == null)
        {
            Debug.LogError("Active order or its ingredients are null!");
            return;
        }

        string s = "Order:\n";

        foreach (Ingredient ing in activeOrder.Ingredients)
            s += " - " + ing + "\n";

        if (orderText != null)
            orderText.text = s;
    }

    public void AddIngredient(Ingredient newIngredient)
    {
        if (!orderStarted || activeOrder == null)
            return;

        if (beingBuilt.Contains(newIngredient))
            return;

        beingBuilt.Add(newIngredient);

        if (activeOrder.Ingredients.Count != beingBuilt.Count)
            return;

        // add sound effect here

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

        orderText.text = "Complete!\n+ $20";
        PlayerInventory.Money += 20;

        // start delayed transition
        StartCoroutine(HandleOrderComplete());
    }

    private IEnumerator HandleOrderComplete()
    {
        // prevent input during transition
        isWaitingForInput = false;
        orderStarted = false;

        yield return new WaitForSeconds(doneTime);

        beingBuilt.Clear();

        // Remove completed sandwich
        if (SandwichOptions.Count > 0)
            SandwichOptions.RemoveAt(0);

        if (orderText != null)
            orderText.text = string.Empty;

        PrintNextDialogue();

        isWaitingForInput = true;
    }
}
