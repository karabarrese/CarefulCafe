using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Collections.Generic; 
using System.Collections;
using UnityEngine.SceneManagement;

public class BakingBowl : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // variables for baking
    private RectTransform rectTransform;
    private Vector2 offset; 
    [SerializeField] private RectTransform targetArea; 
    [SerializeField] private Dialogue dialogue; 
    [SerializeField] private Sprite managerDialogueSprite;
    [SerializeField] private CursorChanger cursor;
    private bool doneBaking = false;

    // variables to animate baking/door
    [SerializeField] private Image croissant;
    public Image door;
    private Vector3 doorTargetPosition;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
        DialogueComponent instruction  = new DialogueComponent(CharacterEmotion.None, "To bake your croissant, drag and drop your batter into the oven.", managerDialogueSprite);
        dialogueArray.Add(instruction);
        DialogueComponent advice  = new DialogueComponent(CharacterEmotion.None, "To avoid cross-contamination, remember to always thoroughly clean baking surfaces and use separate trays or parchment paper.", managerDialogueSprite);
        dialogueArray.Add(advice);
        dialogue.UpdateFullDialogue(dialogueArray);

        doorTargetPosition = door.rectTransform.position;
    }

    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.E) && doneBaking){
            PlayerPrefs.SetInt("DoneWithMinigame", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("KitchenScene");
        } 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        Vector2 localPointerPosition = eventData.position - (Vector2)rectTransform.position;
        offset = localPointerPosition;
        cursor.SetGlovedCursor();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 targetPosition = eventData.position - offset;
        rectTransform.position = targetPosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (targetArea != null && RectTransformUtility.RectangleContainsScreenPoint(targetArea, eventData.position))
        {
            if (!doneBaking){
                doneBaking = true;
                // TODO: door
                UpdateWhenBake();
            }
        }
    }

    private void UpdateWhenBake()
    {
        door.gameObject.SetActive(true);
        StartCoroutine(DelayedAction());
    }

    private IEnumerator DelayedAction()
    {
        // Wait for 3 seconds before showing croissant/text box
        yield return new WaitForSeconds(3f);

        croissant.gameObject.SetActive(true);
        GetComponent<Image>().enabled = false;

        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();
        DialogueComponent goodJob = new DialogueComponent(CharacterEmotion.Heart, "Good job baking a croissant! Now, press 'E' to return to the kitchen.", managerDialogueSprite);
        dialogueArray.Add(goodJob);
        dialogue.UpdateFullDialogue(dialogueArray);
    }

}
