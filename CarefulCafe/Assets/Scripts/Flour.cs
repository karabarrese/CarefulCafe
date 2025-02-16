using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flour : MonoBehaviour
{
    public uiManager uiManagerScript; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseDown()
    {
        uiManagerScript.openFlourUI(); // Calls the UI function
    }
}

