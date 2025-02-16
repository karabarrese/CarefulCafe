using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaucetScript : MonoBehaviour
{
    public bool faucetPress;
    public Animator animator;
    [SerializeField] private BubbleSpawnerScript BubbleSpawnerScript;

    // Start is called before the first frame update
    void Start()
    {
        faucetPress = false;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("FaucetPress", faucetPress);
    }
    private void OnMouseDown()
    {
        //capture mouse position offset
        faucetPress = true;
        BubbleSpawnerScript.dissolveBubbles();
        // Debug.Log("Sink on.");
    }

    private void OnMouseUp()
    {
        //capture mouse position offset
        faucetPress = false;
    }
}
