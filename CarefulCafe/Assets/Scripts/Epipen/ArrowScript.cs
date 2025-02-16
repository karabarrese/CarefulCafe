using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public BarScript bar; // Reference to Bar object
    public Rigidbody2D rigid; // Reference to the Rigidbody2D component of the arrow
    public GameObject epipen; // Reference to the epipen object
    public float speed; // Speed of the arrow's movement
    private bool movingUp = true; // Whether the arrow is moving up or down
    private float currentY; // Current Y position of the arrow

    private bool arrowisMoving = true; // Flag to control whether the arrow is moving

    void Start()
    {
        bar = GameObject.FindGameObjectWithTag("Bar").GetComponent<BarScript>(); // Find the Bar object and get its script
        currentY = transform.position.y + 2; // Get the initial Y position of the arrow
        Debug.Log("Current Y position of the arrow is: " + currentY);
    }

    void Update()
    {
        // If spacebar is pressed, stop the arrow
        if (Input.GetKeyDown(KeyCode.Space))
        {
            arrowisMoving = false; // Changes the flag to stop the arrow
            rigid.velocity = Vector2.zero; // Stops the arrow's movement
                                           // Move arrow to left by a few pixels after stopping
            StartCoroutine(MoveArrowAfterStop()); // Start coroutine to move arrow after stopping
            // Move epipen to the left.
            epipen.transform.Translate(Vector2.left * 0.5f);

        }
        if (arrowisMoving)
        {
            // Calls MoveArrow() function to move the arrow
            MoveArrow();
        }
    }

    IEnumerator MoveArrowAfterStop()
    {
        yield return new WaitForSeconds(0.1f); // Wait for a short duration
        transform.Translate(Vector2.left * 1f); // Move arrow to left by a few pixels
        yield return new WaitForSeconds(2f); // Wait for a short duration
        transform.Translate(Vector2.right * 1f); // Move arrow to right by a few pixels
    }

    void MoveArrow()
    {
        // Debug.Log("Arrow is moving :()");
        // Debug.Log("Height of the bar is: " + bar.GetHeight());

        // Get the height of the bar
        float barHeight = bar.GetHeight();

        // Checks if the arrow is moving up or down
        if (movingUp)
        {
            // If the arrow has reached the top of the bar, switch direction
            if (transform.position.y >= currentY + barHeight + 1)
            {
                movingUp = false;
            }
            else
            {
                // Move the arrow up
                transform.Translate(Vector2.up * speed * Time.deltaTime);
            }
        }
        else
        {
            // If the arrow has reached the bottom of the bar, switch direction
            if (transform.position.y <= currentY - barHeight)
            {
                movingUp = true;
            }
            else
            {
                // Move the arrow down
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
        }
    }

}
