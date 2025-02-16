using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetScript : MonoBehaviour
{
    public bool faucetPress;

    // Start is called before the first frame update
    void Start()
    {
        faucetPress = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        //capture mouse position offset
        faucetPress = true;
        Debug.Log("Sink on.");
    }
}
