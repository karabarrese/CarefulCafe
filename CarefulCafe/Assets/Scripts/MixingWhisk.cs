using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic; 
using UnityEngine.SceneManagement;

public class MixingWhisk : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    // Variables to drag whisk
    private RectTransform rectTransform; 
    private Image image; 
    private Vector2 offset; 

    // Variables to track how long whisk has been mixing
    private float dragStartTime; 
    private bool isDragging = false; 
    private float mixingTargetDuration = 5f;
    private bool doneMixing = false;

    // variables to update which images are displayed once done mixing
    [SerializeField] private Image flour;
    [SerializeField] private Image milk;
    [SerializeField] private Image egg;
    [SerializeField] private Image mix;
    [SerializeField] private Dialogue dialogueBox;
    [SerializeField] private Sprite managerDialogueSprite;

    public void OnBeginDrag(PointerEventData eventData)
    {
        // makes image transparent
        image.color = new Color32(255, 255, 255, 170);

        // offset from the pointer position to the center of the image
        Vector2 localPointerPosition = eventData.position - (Vector2)rectTransform.position;
        offset = localPointerPosition;

        // track drag time
        dragStartTime = Time.time;
        isDragging = true;
    }

    public void OnDrag(PointerEventData eventData)
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;

        Vector2 targetPosition = eventData.position - offset;

        float canvasWidth = rectTransform.rect.width;
        float canvasHeight = rectTransform.rect.height;

        // Bowl position limits 
        float topLimit = 0.8f * screenHeight;
        float bottomLimit = 0.25f * screenHeight;
        float leftLimit = 0.5f * screenWidth;  
        float rightLimit = 0.65f * screenWidth;

        // make sure whisk doesn't leave constraints
        if (targetPosition.y < bottomLimit)
        {
            targetPosition.y = bottomLimit;
        }
        else if (targetPosition.y > topLimit)
        {
            targetPosition.y = topLimit;
        }

        if (targetPosition.x < leftLimit)
        {
            targetPosition.x = leftLimit;
        }
        else if (targetPosition.x > rightLimit)
        {
            targetPosition.x = rightLimit;
        }

        // update position of image
        rectTransform.position = targetPosition;

        // check if target time has passed since drag started
        if (isDragging && Time.time - dragStartTime >= mixingTargetDuration)
        {
            if (!doneMixing){
                UpdateWhenDoneMixing();
                doneMixing = true;
            }
           
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // reset to default color
        image.color = new Color(255, 255, 255, 255);

        // Stop tracking drag
        isDragging = false;
    }

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && doneMixing){
            PlayerPrefs.SetInt("DoneWithMinigame", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("KitchenScene");
        } 
    }

    void UpdateWhenDoneMixing()
    {
        flour.gameObject.SetActive(false);
        milk.gameObject.SetActive(false);
        egg.gameObject.SetActive(false);
        mix.gameObject.SetActive(true);
        // TODO: particle effect UI

        // Display manager text
        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
        DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "Good job mixing the ingredients! Now press 'E' to return to the kitchen", managerDialogueSprite);
        dialogueArray.Add(instruction);
        dialogueBox.UpdateFullDialogue(dialogueArray);
    }
}
