using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Egg : MonoBehaviour
{
    public uiManager uiManagerScript; 
    public GameObject egg1;
    public GameObject egg2;
    public GameObject egg3;
    public GameObject box1;
    public GameObject box2;
    public bool yes = true;

    // Start is called before the first frame update
    void Start()
    {
        box1.SetActive(false);
        box2.SetActive(false);
        egg1.SetActive(false);
        egg2.SetActive(false);
        egg3.SetActive(false);
        yes = true;
        gameObject.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void OnMouseDown()
    {
        if (yes)
        {
        box1.SetActive(true);
        box2.SetActive(true);
        egg1.SetActive(true);
        egg2.SetActive(true);
        egg3.SetActive(true);
        yes = false;
        }
        gameObject.SetActive(false);
        //uiManagerScript.openEggUI(); // Calls the UI function
    }
}
