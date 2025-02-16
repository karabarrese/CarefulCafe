using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class UIManager : MonoBehaviour
{
    // Game manager variables
    public static GameManager Instance { get; private set; }
    public int score = 0; // TODO: customers served? money made? badge?
    [SerializeField] private List<CustomerInfo> customers;
    private int curCustomerIndex = 0;
    [SerializeField] private Sprite managerDialogueSprite;
    [SerializeField] private Sprite playerDialogueSprite;

    // Keep track of step

    public enum Step {NONE, GET_ORDER, ORDER, PANTRY_MINIGAME, BAKING_MINIGAME, WASHING_MINIGAME, GIVE_ORDER, GAME_OVER}
    private Step curStep;
    private Step prevStep = Step.NONE;

    // Variables for scene
    [SerializeField] private GameObject player;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private CursorChanger cursorChanger;

    // Colliders  for different steps
    [SerializeField] private OrderTriggerHandler orderTrigger;

    private void Start(){
        curStep = Step.GET_ORDER;
        curCustomerIndex = 0;
    }

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update(){
        switch (curStep){
            case Step.GET_ORDER:
                if (prevStep != curStep){
                    prevStep = curStep;
                    if(curCustomerIndex == 0){ // manager gives instructions on first playthrough
                        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();

                        DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Welcome to your first day or work! Begin by walking over to the register area and press 'E' to take your customer's order!", managerDialogueSprite);
                        dialogueArray.Add(instruction);
                        DialogueComponent allergyInstruction  = new DialogueComponent(CharacterEmotion.None, "Remember to ask about alergies!", managerDialogueSprite);
                        dialogueArray.Add(allergyInstruction);

                        dialogue.UpdateFullDialogue(dialogueArray);
                    }
                }
                
                if(orderTrigger.IsPlayerInside() && Input.GetKeyDown(KeyCode.E)){
                    curStep = Step.ORDER;
                }
                break;
            case Step.ORDER:
                if (curStep != prevStep){
                    OrderDialogue();
                    prevStep = curStep;
                }
                
                if (dialogue.IsTextDone()){ // TODO, make user play game
                    // curStep = Step.PANTRY_MINIGAME; // TODO: always go to PANTRY_MINIGAME
                    if(curCustomerIndex == 0){ 
                        curStep = Step.PANTRY_MINIGAME;
                    } else {
                        curStep = Step.GIVE_ORDER;
                    }
                }

                

                break;
            case Step.PANTRY_MINIGAME:
                if(curStep != prevStep){
                    // TODO: add glow effect, add collider
                    if(curCustomerIndex == 0){ // manager gives instructions on first playthrough
                        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                        DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Good job taking that order! Now head over to the pantry and click 'E' to get the ingredients you need!", managerDialogueSprite);
                        dialogueArray.Add(instruction);
                        dialogue.UpdateFullDialogue(dialogueArray);
                    }

                    prevStep = curStep;
                }
                
                if (dialogue.IsTextDone()){
                    curStep = Step.BAKING_MINIGAME;
                }
                break;
            case Step.BAKING_MINIGAME:
                if(curStep != prevStep){
                    if(curCustomerIndex == 0){ // manager gives instructions on first playthrough
                        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                        DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Next, head over to the oven to bake your croissant!", managerDialogueSprite);
                        dialogueArray.Add(instruction);
                        dialogue.UpdateFullDialogue(dialogueArray);
                    }
                    prevStep = curStep;
                }

                if (dialogue.IsTextDone()){
                    curStep = Step.WASHING_MINIGAME;
                }
                
                break;
            case Step.WASHING_MINIGAME:
                if(curStep != prevStep){
                    if(curCustomerIndex == 0){ // manager gives instructions on first playthrough
                        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                        DialogueComponent goodJob  = new DialogueComponent(CharacterEmotion.Heart, "Good job at successfully baking a croissant!", managerDialogueSprite);
                        dialogueArray.Add(goodJob);
                        DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Now, head over to the sink and press 'E' to wash you dishes to avoid cross-contamination in the future!", managerDialogueSprite);
                        dialogueArray.Add(instruction);
                        dialogue.UpdateFullDialogue(dialogueArray);
                    }
                    prevStep = curStep;
                }

                if (dialogue.IsTextDone()){
                    curStep = Step.GIVE_ORDER;
                }

                break;
            case Step.GIVE_ORDER:
                if (curStep != prevStep){ // TODO: actually give order
                     if(curCustomerIndex == 0){ // manager gives instructions on first playthrough
                        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                        DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.Heart, "Great! Now, head over to the cash register to give your customer their croissant, and repeat the process for all customers!", managerDialogueSprite);
                        dialogueArray.Add(instruction);
                        dialogue.UpdateFullDialogue(dialogueArray);
                    }

                    if (curCustomerIndex == customers.Count - 1){
                        curStep = Step.GAME_OVER;
                    } else{
                        curCustomerIndex++;
                        curStep = Step.GET_ORDER;
                    }

                    prevStep = curStep;
                    
                }
                break;
                
            case Step.GAME_OVER:
                break;
        }
        Debug.Log(curStep);
    }

    public void ChangeToPantryMinigame(){
        curStep = Step.PANTRY_MINIGAME;

        if (cursorChanger != null){
            cursorChanger.SetDefaultCursor();
        }
    }

    public void OrderDialogue(){
        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  // Create an empty list

        DialogueComponent welcomeDC = new DialogueComponent(CharacterEmotion.None, "Hi! Welcome to CarefulCafe! May I take your order?", playerDialogueSprite);
        dialogueArray.Add(welcomeDC);

        if(customers[curCustomerIndex].CusAllergy == Allergy.None){
            DialogueComponent orderDC = new DialogueComponent(CharacterEmotion.None, $"Hi! I'm {customers[curCustomerIndex].Name}, and I would like to order a Croissant.", customers[curCustomerIndex].TextImg);
            dialogueArray.Add(orderDC);

            DialogueComponent allergyQuestionDC = new DialogueComponent(CharacterEmotion.None, "Of course! Do you have any allergies we should be aware of?", playerDialogueSprite);
            dialogueArray.Add(allergyQuestionDC);

            DialogueComponent noAllergyDC = new DialogueComponent(CharacterEmotion.None, "Nope!", customers[curCustomerIndex].TextImg);
            dialogueArray.Add(noAllergyDC);

            DialogueComponent prepareFoodDC = new DialogueComponent(CharacterEmotion.None, "Great! Your order will be ready shortly!", playerDialogueSprite);
            dialogueArray.Add(prepareFoodDC);
        } else {
            DialogueComponent orderDC = new DialogueComponent(CharacterEmotion.Sweat, $"Hi! I'm {customers[curCustomerIndex].Name}, and I would like to order a Croissant. However, I'm allergic to {customers[curCustomerIndex].CusAllergy}", customers[curCustomerIndex].TextImg);
            dialogueArray.Add(orderDC);

            DialogueComponent allergyQuestionDC = new DialogueComponent(CharacterEmotion.None, "Sure thing, we'll make the appropriate accomadations! What is your allegy severity level?", playerDialogueSprite);
            dialogueArray.Add(allergyQuestionDC);

            DialogueComponent severityDC = new DialogueComponent(CharacterEmotion.None, customers[curCustomerIndex].SeverityStatement, customers[curCustomerIndex].TextImg);
            dialogueArray.Add(severityDC);

            DialogueComponent prepareFoodDC = new DialogueComponent(CharacterEmotion.None, "Sounds good! Your order will be ready shortly!", playerDialogueSprite);
            dialogueArray.Add(prepareFoodDC);
        }

        dialogue.UpdateFullDialogue(dialogueArray);
    }
}
