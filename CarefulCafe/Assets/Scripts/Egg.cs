using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public uiManager uiManagerScript; 
    public GameObject egg1;
    public GameObject egg2;
    public GameObject egg3;
    public GameObject box1;
    public GameObject box2;
    public bool hasAllergy;
    public string allergy;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Sprite managerDialogueSprite;
    public int customerCount;

    // Start is called before the first frame update
    void Start()
    {
        customerCount = PlayerPrefs.GetInt("curCustomerIndex",-1);
        if (customerCount==0)
        {
            List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
            DialogueComponent instruction1  = new DialogueComponent(CharacterEmotion.None, "Here's where we make our signature kitten kroissant dough (tm)! Make sure to add a form of eggs, flour, and milk to create it. ", managerDialogueSprite);
            dialogueArray.Add(instruction1);
            DialogueComponent instruction2  = new DialogueComponent(CharacterEmotion.None, "It's also very important to keep ingredients that may cause reactions to different people in sealed containers and be very cautious when reading ingredient labels", managerDialogueSprite);
            dialogueArray.Add(instruction2);
            DialogueComponent instruction3  = new DialogueComponent(CharacterEmotion.None, "When reading, even things like 'made in a factory that contains traces of peanuts' could be detrimental to someone's health. To protect against cross contamination, we have these sealed containers.", managerDialogueSprite);
            dialogueArray.Add(instruction3);
            DialogueComponent instruction4  = new DialogueComponent(CharacterEmotion.None, "For those with egg allergies, use an egg from plant as substitute", managerDialogueSprite);
            dialogueArray.Add(instruction4);
            DialogueComponent instruction5  = new DialogueComponent(CharacterEmotion.None, "For those with gluten allergies, use almond flour.", managerDialogueSprite);
            dialogueArray.Add(instruction5);
            DialogueComponent instruction6  = new DialogueComponent(CharacterEmotion.None, "For those allergic to dairy products, use almond milk", managerDialogueSprite);
            dialogueArray.Add(instruction6);
            DialogueComponent instruction7  = new DialogueComponent(CharacterEmotion.None, "For those allergic to nuts, use regular milk and regular flour.", managerDialogueSprite);
            dialogueArray.Add(instruction7);
            dialogue.UpdateFullDialogue(dialogueArray);
        }
        box1.SetActive(false);
        box2.SetActive(false);
        egg1.SetActive(false);
        egg2.SetActive(false);
        egg3.SetActive(false);
        allergy = PlayerPrefs.GetString("CurPlayerAllergy");
        if(allergy == "egg")
        {
            hasAllergy = true;
        }
        else
        {
            hasAllergy = false;
        }
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseDown()
    {
        if (hasAllergy)
        {
            List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
            DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Oops, we don't want to add the eggs to this recipe because our customer is allergic to them!", managerDialogueSprite);
            dialogueArray.Add(instruction);
            dialogue.UpdateFullDialogue(dialogueArray);
        }
        else
        {
            box1.SetActive(true);
            box2.SetActive(true);
            egg1.SetActive(true);
            egg2.SetActive(true);
            egg3.SetActive(true);
            gameObject.SetActive(false);
            
        }
        
        //uiManagerScript.openEggUI(); // Calls the UI function
    }
}
