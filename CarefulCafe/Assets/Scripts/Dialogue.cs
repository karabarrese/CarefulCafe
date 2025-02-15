using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textComponent;
    [SerializeField] private Button nextButton;
    [SerializeField] private Canvas dialogueCanvas;
    [SerializeField] private string[] lines;
    [SerializeField] private float textSpeed;

    private int index;

    // Start is called before the first frame update
    void Start()
    {
        textComponent.text = string.Empty;
        nextButton.gameObject.SetActive(false); // hide button
        nextButton.onClick.AddListener(OnNextButtonPressed); // track if button is clicked
        StartDialogue();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
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
        }
    }
}
