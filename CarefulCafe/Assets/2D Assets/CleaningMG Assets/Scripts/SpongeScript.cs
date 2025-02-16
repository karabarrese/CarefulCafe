using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpongeScript : MonoBehaviour
{
    public Animator animator;
    public bool soaped;
    Vector3 mousePositionOffset;
    public float defaultX;
    public float defaultY;

    // dialogue
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Sprite managerDialogueSprite;
    private bool noBubblesPromptYet = true;

    // Start is called before the first frame update
    void Start()
    {
        List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
        DialogueComponent goodJob  = new DialogueComponent(CharacterEmotion.None, "Drag the soap bottle over to the sponge to add soap to it.", managerDialogueSprite);
        dialogueArray.Add(goodJob);
        dialogue.UpdateFullDialogue(dialogueArray);

        animator.SetBool("Soaped", false);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("Soaped", soaped);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Soap Bottle")
        {
            soaped = true;

            if (noBubblesPromptYet){
                noBubblesPromptYet = false;
                List<DialogueComponent> dialogueArray = new List<DialogueComponent>();  
                DialogueComponent goodJob  = new DialogueComponent(CharacterEmotion.None, "Scrub the whisk with the sponge.", managerDialogueSprite);
                dialogueArray.Add(goodJob);
                dialogue.UpdateFullDialogue(dialogueArray);
            }
            // Debug.Log("soaped");
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse position & return world point
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    private void OnMouseDown()
    {
        //capture mouse position offset
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
        // Debug.Log("Grabbing object.");
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
        // Debug.Log("Dragging object.");
    }

    private void OnMouseUp()
    {
        transform.position = new Vector2(defaultX, defaultY);
        // Debug.Log("Letting go of object.");
    }

}
