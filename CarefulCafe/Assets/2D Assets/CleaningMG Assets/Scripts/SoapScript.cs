using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using tutorial:  https://www.youtube.com/watch?v=yalbvB84kCg

public class DraggableItem : MonoBehaviour
{
    Vector3 mousePositionOffset;
    public float defaultX;
    public float defaultY;
    public Animator animator;
    public bool HoveringOverSponge;
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

    // Start is called before the first frame update
    void Start()
    {
        animator.SetBool("HoveringOverSponge", false);
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("HoveringOverSponge", HoveringOverSponge);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sponge")
        {
            HoveringOverSponge = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Sponge")
        {
            HoveringOverSponge = false;
        }
    }
}
