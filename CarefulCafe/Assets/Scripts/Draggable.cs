using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using tutorial:  https://www.youtube.com/watch?v=yalbvB84kCg

public class Draggable : MonoBehaviour
{
   Vector3 mousePositionOffset;
   // Start is called before the first frame update
    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse position & return world point
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown() {
    
    //capture mouse position offset
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }
    private void OnMouseDrag() {
        transform.position = GetMouseWorldPosition() + mousePositionOffset;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
