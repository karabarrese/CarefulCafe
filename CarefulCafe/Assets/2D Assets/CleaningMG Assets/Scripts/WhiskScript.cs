using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WhiskScript : MonoBehaviour
{
    Vector3 mousePositionOffset;
    public float defaultX;
    public float defaultY;
    public Animator animator;
    public bool spongeHover; // if sponge is enlarged or not
    public bool spongeHoverStay; // if sponge is constantly touching whisk

    public bool bubbleCovered;
    public bool rinsed;
    [SerializeField] private SpongeScript Sponge;
    [SerializeField] private BubbleSpawnerScript BubbleSpawner;

    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse position & return world point
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown()
    {
        //capture mouse position offset
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
        //Debug.Log("Grabbing object.");
    }
    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
        //Debug.Log("Dragging object.");
    }

    private void OnMouseUp() 
    {
        if (bubbleCovered == false && rinsed == false)
        {
            transform.position = new Vector2(defaultX, defaultY); // snap to original position
        }
        //Debug.Log("Letting go of object.");
    }

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("SpongeHover", false);
        rinsed = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("SpongeHover", spongeHover);
        animator.SetBool("Clean", bubbleCovered);
        bubbleCovered = BubbleSpawner.maxBubblesReached;
        rinsed = BubbleSpawner.bubblesGone;
        if (rinsed)
        {
            Debug.Log("rinsed");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sponge" && Sponge.soaped == true && bubbleCovered == false)
        { // if sponge touches whisk and the sponge has soap on it
            spongeHover = true;
            // Debug.Log("Hovering over sponge.");
        }
        // Debug.Log(collision.gameObject.name);
        if (collision.gameObject.name == "Dishwasher" && rinsed)
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false;

            Debug.Log("Game finished!");
            PlayerPrefs.SetInt("DoneWithMinigame", 1);
            PlayerPrefs.Save();
            SceneManager.LoadScene("KitchenScene");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.name == "Sponge")
        {
            spongeHoverStay = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (bubbleCovered == true)
        {
            spongeHover = false;
        }
        spongeHoverStay = false;
    }

}
