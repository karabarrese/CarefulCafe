using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum CharacterEmotion {None, Sweat, Heart}

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private Button nextButton;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private string[] lines;
    [SerializeField] private List<DialogueComponent> dialogueComponents;
    [SerializeField] private float textSpeed;
    [SerializeField] private Image speakerImg;

    private int index;
    private bool isTextDone = true;
    private bool onUpdateLinesMode = true;

    private CharacterEmotion curEmotion = CharacterEmotion.None;
    [SerializeField] private Image SweatImg;
    [SerializeField] private Image HeartImg;
    private Dialogue Instance;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(OnNextButtonPressed); // track if button is clicked
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void StartDialogue()
    {
        if (nextButton == null || dialogueCanvas == null){
            return;
        }

        isTextDone = false;
        textComponent.text = string.Empty;
        nextButton.gameObject.SetActive(false); // hide button
        index = 0;
        dialogueCanvas.gameObject.SetActive(true);

        if (onUpdateLinesMode){
            UpdateCharacterEmotionImg();
            StartCoroutine(TypeLine());
        } else {
            SetCurEmotion(dialogueComponents[index].Emotion);
            SetSpeakerImg(dialogueComponents[index].SpeakerImage);
            StartCoroutine(TypeDialogueComponentLine());
        }
    }

    // HELPER FUNCTIONS TO UPDATE TEXT BOX COMPONENTS
    private void UpdateCharacterEmotionImg()
    {
        if (curEmotion == CharacterEmotion.None){
            SweatImg.enabled = false;
            HeartImg.enabled = false;
        } else if (curEmotion == CharacterEmotion.Sweat){
            SweatImg.enabled = true;
            HeartImg.enabled = false;
        } else if (curEmotion == CharacterEmotion.Heart){
            SweatImg.enabled = false;
            HeartImg.enabled = true;
        }
    }

    public void SetCurEmotion(CharacterEmotion newEmotion){
        curEmotion = newEmotion;
        UpdateCharacterEmotionImg();
    }

    public void SetSpeakerImg(Sprite newSpeaker){
        if (speakerImg != null && newSpeaker != null)
        {
            speakerImg.sprite = newSpeaker;
        }
    }

    public void HideTextBox(){
        if (dialogueCanvas != null){
            dialogueCanvas.gameObject.SetActive(false);
        }
        isTextDone = true;  
    }

    // FUNCTIONS TO UPDATE DIALOGUE
    public void UpdateText(string[] newLines){
        lines = newLines;
        onUpdateLinesMode = true;
        StartDialogue();
    }

    public void UpdateFullDialogue(List<DialogueComponent> newConversation){
        dialogueComponents = newConversation;
        onUpdateLinesMode = false;
        StartDialogue();
    }

    // FUNCTIONS TO PRINT TEXT
    IEnumerator TypeLine()
    {
        if (!onUpdateLinesMode) yield break;
        foreach(char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // show button when line of text is fully displayed
        nextButton.gameObject.SetActive(true);
    }

    IEnumerator TypeDialogueComponentLine()
    {
        if (onUpdateLinesMode) yield break;
        foreach(char c in dialogueComponents[index].DialogueText.ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(textSpeed);
        }

        // show button when line of text is fully displayed
        nextButton.gameObject.SetActive(true);
    }

    public void OnNextButtonPressed()
    {
        // Hide the button again for the next line
        nextButton.gameObject.SetActive(false);

        // Increment the index and display if there is additional text
        index++;

        if (onUpdateLinesMode){
            if (index < lines.Length)
            {
                textComponent.text = string.Empty;
                StartCoroutine(TypeLine());
            }
            else
            {
                HideTextBox();
            }
        } else {
            if (index < dialogueComponents.Count) {
                textComponent.text = string.Empty;
                SetCurEmotion(dialogueComponents[index].Emotion);
                SetSpeakerImg(dialogueComponents[index].SpeakerImage);

                StartCoroutine(TypeDialogueComponentLine());
            } else {
                HideTextBox();
            }
        }
    }

    // FOR EXTERNAL PURPOSES    

    public bool IsTextDone(){
        return isTextDone;
    }
}
public class DialogueComponent
{
    public CharacterEmotion Emotion { get; set; }
    public string DialogueText { get; set; }
    public Sprite SpeakerImage { get; set; }

    public DialogueComponent(CharacterEmotion emotion, string dialogueText, Sprite speakerImage)
    {
        Emotion = emotion;
        DialogueText = dialogueText;
        SpeakerImage = speakerImage;
    }
}
