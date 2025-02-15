using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public BarScript barScript;
    public Rigidbody2D arrowrigidbody;
    public bool arrowisMoving = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // while bool is true, the arrow will move up and down to a certain height and will stop once user clicks on space bar
        if (arrowrigidbody.position.y > -3.5)
        {
            Debug.Log("Arrow has reached the bottom");
            arrowrigidbody.velocity = Vector2.up * 2;
        }
        else if (arrowrigidbody.position.y <= 3.5)
        {
            Debug.Log("Arrow has reached the top");
            arrowrigidbody.velocity = Vector2.down * 2;
        }

        if (Input.GetKeyDown(KeyCode.Space) == true)
        {
            arrowisMoving = false;
            arrowrigidbody.velocity = Vector2.zero;
        }


    }
}
