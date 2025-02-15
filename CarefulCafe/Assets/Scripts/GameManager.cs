using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Game manager variables
    public static GameManager Instance { get; private set; }
    public int score = 0; // TODO: customers served? money made?

    // Keep track of step

    public enum Step {ORDER, PANTRY_MINIGAME, BAKING_MINIGAME, WASHING_MINIGAME, GIVE_ORDER}
    private Step curStep;
    private Step prevStep;

    // Variables for scene
    [SerializeField] private GameObject player;

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
            case Step.ORDER:
                break;
            case Step.PANTRY_MINIGAME:
                break;
            case Step.BAKING_MINIGAME:
                break;
            case Step.WASHING_MINIGAME:
                break;
            case Step.GIVE_ORDER:
                break;
        }
    }
}
