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
    [SerializeField] private float textSpeed;

    private int index;
    private bool isTextDone = true;

    private CharacterEmotion curEmotion = CharacterEmotion.None;
    [SerializeField] private Image SweatImg;
    [SerializeField] private Image HeartImg;

    // Start is called before the first frame update
    void Start()
    {
        nextButton.onClick.AddListener(OnNextButtonPressed); // track if button is clicked
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartDialogue()
    {
        if (nextButton == null || dialogueCanvas == null){
            return;
        }

        UpdateCharacterImg();
        isTextDone = false;
        textComponent.text = string.Empty;
        nextButton.gameObject.SetActive(false); // hide button
        index = 0;
        dialogueCanvas.gameObject.SetActive(true);
        StartCoroutine(TypeLine());
    }

    private void UpdateCharacterImg()
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
        UpdateCharacterImg();
    }

    IEnumerator TypeLine()
    {
        foreach(char c in lines[index].ToCharArray())
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
        if (index < lines.Length)
        {
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            // hide text box
            if (dialogueCanvas != null){
                dialogueCanvas.gameObject.SetActive(false);
            }
            isTextDone = true;
        }
    }

    public void UpdateText(string[] newLines){
        lines = newLines;
        StartDialogue();
    }

    public bool IsTextDone(){
        return isTextDone;
    }
}
