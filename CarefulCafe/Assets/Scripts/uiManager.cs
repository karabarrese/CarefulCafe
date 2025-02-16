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
        //Debug.Log(PlayerPrefs.GetString("CurPlayerAllergy", "you failed"));
    }
    
    public void clickAdd()
    {
        //switch case, 1 = almond flour, 2 = flour, 3 = milk, 4 = almond milk, 5 = not egg
        switch(whatIngredients){
            case 1: //almond flour
                if (flourIn)
                    {
                        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                        DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "That's a bit too much flour", managerDialogueSprite);
                        dialogueArray.Add(instruction);
                        dialogue.UpdateFullDialogue(dialogueArray);
                        closeUI();
                    }
                    else if (glutenAllergy)
                    {
                        flourMixture.SetActive(true);
                        numIngredients++;
                        flourIn = true;
                        closeUI();
                    }
                    else
                    {
                        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                        DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Oops, we don't want to add the almond flour to this recipe to save it for those who need it!", managerDialogueSprite);
                        dialogueArray.Add(instruction);
                        dialogue.UpdateFullDialogue(dialogueArray);
                        closeUI();
                    }
                    break;
            case 2: //flour
                if (flourIn)
                {
                    List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                    DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "That's a bit too much flour", managerDialogueSprite);
                    dialogueArray.Add(instruction);
                    dialogue.UpdateFullDialogue(dialogueArray);
                    closeUI();
                }
                else if (!glutenAllergy)
                {
                    flourMixture.SetActive(true);
                    numIngredients++;
                    flourIn = true;
                    closeUI();
                }
                else
                {
                    List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                    DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Oops, we don't want to add the wheat flour to this recipe because our customer is allergic to gluten!", managerDialogueSprite);
                    dialogueArray.Add(instruction);
                    dialogue.UpdateFullDialogue(dialogueArray);
                    closeUI();
                }
                break;

            case 3: //milk
                if (milkIn)
                {
                    List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                    DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "That's a bit too much milk", managerDialogueSprite);
                    dialogueArray.Add(instruction);
                    dialogue.UpdateFullDialogue(dialogueArray);
                    closeUI();
                }
                else if (!dairyAllergy)
                {
                    milkMixture.SetActive(true);
                    numIngredients++;
                    milkIn = true;
                    closeUI();
                }
                else
                {
                    List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                    DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Oops, we don't want to add the regular milk to this recipe because our customer is allergic to dairy!", managerDialogueSprite);
                    dialogueArray.Add(instruction);
                    dialogue.UpdateFullDialogue(dialogueArray);
                    closeUI();
                }
                break;
            case 4: //almond milk
                if (milkIn)
                {
                    List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                    DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "That's a bit too much milk", managerDialogueSprite);
                    dialogueArray.Add(instruction);
                    dialogue.UpdateFullDialogue(dialogueArray);
                    closeUI();
                }
                else if (dairyAllergy)
                {
                    milkMixture.SetActive(true);
                    numIngredients++;
                    milkIn = true;
                    closeUI();
                }
                else
                {
                    List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                    DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Oops, we don't want to add the almond milk to this recipe to save it for those who need it!", managerDialogueSprite);
                    dialogueArray.Add(instruction);
                    dialogue.UpdateFullDialogue(dialogueArray);
                    closeUI();
                }
                break;
            case 5: //not egg
                if (eggIn)
                {
                    List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                    DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "That's a bit too much 'not egg'", managerDialogueSprite);
                    dialogueArray.Add(instruction);
                    dialogue.UpdateFullDialogue(dialogueArray);
                    closeUI();
                }
                else if(!eggAllergy)
                {
                    eggMixture.SetActive(true);
                    numIngredients++;
                    eggIn = true;
                    closeUI();
                }
                else
                {
                    List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                    DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Oops, let's save the imitation egg to those with allergies", managerDialogueSprite);
                    dialogueArray.Add(instruction);
                    dialogue.UpdateFullDialogue(dialogueArray);
                    closeUI();
                }
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
    public void openAlmondFlourMilkUI()
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
        whatIngredient = 4;
        backToCabinet.SetActive(true);
    }
}
