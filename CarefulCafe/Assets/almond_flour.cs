using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class almond_flour : MonoBehaviour
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
        uiManagerScript.openAlmondFlourUI(); // Calls the UI function
    }
}
