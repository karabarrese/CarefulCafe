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

    // Start is called before the first frame update
    void Start()
    {
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
