using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class GameManager : MonoBehaviour
{
    // Game manager variables
    public static GameManager Instance { get; private set; }
    public int score = 0; // TODO: customers served? money made? badge?
    [SerializeField] private List<CustomerInfo> customers;
    private int curCustomerIndex = 0;
    [SerializeField] private Sprite playerDialogueSprite;
   

    // Keep track of step

    public enum Step {GET_ORDER, ORDER, PANTRY_MINIGAME, BAKING_MINIGAME, WASHING_MINIGAME, GIVE_ORDER, GAME_OVER}
    private Step curStep;
    private Step prevStep;

    // Variables for scene
    [SerializeField] private GameObject player;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private CursorChanger cursorChanger;

    private void Start(){
        curStep = Step.ORDER;
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
                break;
            case Step.ORDER:
                if (curStep != prevStep){
                    OrderDialogue();
                    prevStep = curStep;
                }
                
                if (dialogue.IsTextDone()){
                    curStep = Step.GIVE_ORDER; // for testing
                    // curStep = Step.PANTRY_MINIGAME; // TODO
                }

                break;
            case Step.PANTRY_MINIGAME:
                if(curStep != prevStep){
                    // TODO: add glow effect, track if 1st round for tutorial

                    // to test dialogue, TODO: update to match behavior
                    dialogue.SetCurEmotion(CharacterEmotion.Sweat);
                    string[] nextText = new string[] {"You are now about to play the pantry minigame, so let's pretend this is useful advice", "Good luck!"};
                    if (dialogue != null){
                        dialogue.UpdateText(nextText);
                    }
                    prevStep = curStep;
                }
                
                if (dialogue.IsTextDone()){
                    curStep = Step.BAKING_MINIGAME;
                }
                break;
            case Step.BAKING_MINIGAME:
                // TODO: remove, used to test customers
                if(curStep != prevStep){
                    dialogue.SetCurEmotion(CharacterEmotion.Heart);
                    string customerText = $"Hi {customers[curCustomerIndex].Name}, you have {customers[curCustomerIndex].CusAllergy}";
                    string[] nextText = new string[] { customerText, "yay" };

                    if (dialogue != null){
                        dialogue.UpdateText(nextText);
                    }
                    prevStep = curStep;
                    curCustomerIndex++;
                }

                if (dialogue.IsTextDone()){
                    curStep = Step.WASHING_MINIGAME;
                }
                
                break;
            case Step.WASHING_MINIGAME:
                break;
            case Step.GIVE_ORDER:
                if (curStep != prevStep){ // TODO: actually give order
                    prevStep = curStep;
                    if (curCustomerIndex == customers.Count - 1){
                        curStep = Step.GAME_OVER;
                    } else{
                        curCustomerIndex++;
                        curStep = Step.ORDER;
                    }
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
