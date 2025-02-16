using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class uiManager : MonoBehaviour
{
    public GameObject flourNutrition;
    public GameObject eggNutrition;
    public GameObject almondFlourNutrition;
    public GameObject almondMilkNutrition;
    public GameObject milkNutrition;

    public GameObject Add;

    public GameObject backToCabinet;

    public GameObject flourMixture;
    public GameObject eggMixture;
    public GameObject milkMixture;

    public bool glutenAllergy;
    public bool dairyAllergy;
    public bool eggAllergy;
    public string allergy;
    public bool nutAllergy;
    private bool milkIn;
    private bool flourIn;
    public bool eggIn;

    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Sprite managerDialogueSprite;
    public int numIngredients;
    public int whatIngredient; // 0 = nothing selected, 1 = egg substitute, 2 = milk of any kind, 3 = flour

    // Start is called before the first frame update
    void Start()
    {
        milkIn = false;
        flourIn = false;
        eggIn = false;
        numIngredients = 0;
        closeUI();
        milkMixture.SetActive(false);
        flourMixture.SetActive(false);
        eggMixture.SetActive(false);
        dairyAllergy = false;
        eggAllergy = false;
        glutenAllergy = false;
        nutAllergy = false;
        //testing glutenAllergy
        allergy = PlayerPrefs.GetString("CurPlayerAllergy");
        //allergies: "None, Dairy, Gluten, Egg, Nut"
        if (allergy == "Dairy")
        {
            dairyAllergy = true;
        }
        else if (allergy == "Gluten")
        {
            glutenAllergy = true;
        }
        if (allergy == "Egg")
        {
            eggAllergy = true;
        }    
        else if (allergy == "Nut")
        {
            nutAllergy = true;
        }    
    }


    // Update is called once per frame
    void Update()
    {
        if (numIngredients==3)
        {
            end();
        }
        Debug.Log(numIngredients);
        if (Input.GetKeyDown(KeyCode.E)&&numIngredients>=3){
            PlayerPrefs.SetInt("DoneWithMinigame", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("KitchenScene");
        } 
        Debug.Log(PlayerPrefs.GetString("CurPlayerAllergy", "you failed"));
    }
    private void end()
    {
        numIngredients++;
        // have person say good job now let's go back 
        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
        DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Good job! You got all the ingredients. Click 'E' to go back to the kitchen", managerDialogueSprite);
        dialogueArray.Add(instruction);
        dialogue.UpdateFullDialogue(dialogueArray);
        //going back to kitchen
        if (Input.GetKeyDown(KeyCode.E)){
            PlayerPrefs.SetInt("DoneWithMinigame", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("KitchenScene");
        } 
    }    
    public void clickAdd()
    {
        //switch case, 1 = almond flour, 2 = flour, 3 = milk, 4 = almond milk, 5 = not egg
        switch (whatIngredient)
        {
            case 1: // Almond Flour
                if (flourIn)
                {
                    ShowDialogue("That's a bit too much flour");
                }
                else if (glutenAllergy)
                {
                    flourMixture.SetActive(true);
                    numIngredients++;
                    flourIn = true;
                }
                else
                {
                    ShowDialogue("Oops, we don't want to add the almond flour to this recipe to save it for those who need it!");
                }
                closeUI();
                break;

            case 2: // Regular Flour
                if (flourIn)
                {
                    ShowDialogue("That's a bit too much flour");
                }
                else if (!glutenAllergy)
                {
                    flourMixture.SetActive(true);
                    numIngredients++;
                    flourIn = true;
                }
                else
                {
                    ShowDialogue("Oops, we don't want to add the wheat flour to this recipe because our customer is allergic to gluten!");
                }
                closeUI();
                break;

            case 3: // Regular Milk
                if (milkIn)
                {
                    ShowDialogue("That's a bit too much milk");
                }
                else if (!dairyAllergy)
                {
                    milkMixture.SetActive(true);
                    numIngredients++;
                    milkIn = true;
                }
                else
                {
                    ShowDialogue("Oops, we don't want to add the regular milk to this recipe because our customer is allergic to dairy!");
                }
                closeUI();
                break;

            case 4: // Almond Milk
                if (milkIn)
                {
                    ShowDialogue("That's a bit too much milk");
                }
                else if (dairyAllergy)
                {
                    milkMixture.SetActive(true);
                    numIngredients++;
                    milkIn = true;
                }
                else
                {
                    ShowDialogue("Oops, we don't want to add the almond milk to this recipe to save it for those who need it!");
                }
                closeUI();
                break;

            case 5: // Egg Substitute
                if (eggIn)
                {
                    ShowDialogue("That's a bit too much egg substitute");
                }
                else if (eggAllergy)
                {
                    eggMixture.SetActive(true);
                    numIngredients++;
                    eggIn = true;
                }
                else
                {
                    ShowDialogue("Oops, let's save the imitation egg for those with allergies");
                }
                closeUI();
                break;

            default:
                ShowDialogue("No ingredient selected!");
                closeUI();
                break;
        }
    }


    public void closeUI()
    {
        flourNutrition.SetActive(false);
        eggNutrition.SetActive(false);
        almondMilkNutrition.SetActive(false);
        almondFlourNutrition.SetActive(false);
        milkNutrition.SetActive(false);
        Add.SetActive(false);
        backToCabinet.SetActive(false);
        whatIngredient = -1;
    }

//switch case, 1 = almond flour, 2 = flour, 3 = milk, 4 = almond milk, 5 = not egg
    public void openAlmondFlourUI()
    {
        almondFlourNutrition.SetActive(true);
        Add.SetActive(true);
        whatIngredient = 1;
        backToCabinet.SetActive(true);
    }
    public void openFlourUI()
    {
        flourNutrition.SetActive(true);
        Add.SetActive(true);
        whatIngredient = 2;
        backToCabinet.SetActive(true);
    }
    public void openEggUI()
    {
        eggNutrition.SetActive(true);
        Add.SetActive(true);
        whatIngredient = 5;
        backToCabinet.SetActive(true);
    }
    public void openAlmondMilkUI()
    {
        almondMilkNutrition.SetActive(true);
        Add.SetActive(true);
        whatIngredient = 4;
        backToCabinet.SetActive(true);
    }
    public void openMilkUI()
    {
        milkNutrition.SetActive(true);
        Add.SetActive(true);
        whatIngredient = 3;
        backToCabinet.SetActive(true);
    }
    private void ShowDialogue(string message)
    {
        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();
        DialogueComponent instruction = new DialogueComponent(CharacterEmotion.None, message, managerDialogueSprite);
        dialogueArray.Add(instruction);
        dialogue.UpdateFullDialogue(dialogueArray);
    }
}

