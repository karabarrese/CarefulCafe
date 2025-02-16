using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoapScript : MonoBehaviour
{
    public Animator animator;
    public bool soaped;
    Vector3 mousePositionOffset;
    public float defaultX;
    public float defaultY;

    // Start is called before the first frame update
    void Start()
    {
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
