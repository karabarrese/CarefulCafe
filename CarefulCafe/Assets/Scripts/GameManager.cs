using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; 

public class GameManager : MonoBehaviour
{
    // Game manager variables
    public static GameManager Instance { get; private set; }
    public int score = 0; // TODO: customers served? money made?
    private List<CustomerInfo> customers;
    private int curCustomerIndex = 0;

    // Keep track of step

    public enum Step {ORDER, PANTRY_MINIGAME, BAKING_MINIGAME, WASHING_MINIGAME, GIVE_ORDER}
    private Step curStep;
    private Step prevStep;

    // Variables for scene
    [SerializeField] private GameObject player;
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private CursorChanger cursorChanger;

    private void Start(){
        curStep = Step.ORDER;

        // initialize customer list
        customers = new List<CustomerInfo>
        {
            new CustomerInfo("Kellay", Allergy.None),
            new CustomerInfo("Limmy", Allergy.Dairy),
            new CustomerInfo("Angi", Allergy.Gluten),
            new CustomerInfo("Car", Allergy.Egg),
            new CustomerInfo("Tiffer", Allergy.Dairy),
            new CustomerInfo("Irah", Allergy.Gluten)
        };
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
            case Step.ORDER:
                break;
            case Step.PANTRY_MINIGAME:
                if(curStep != prevStep){
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
}
