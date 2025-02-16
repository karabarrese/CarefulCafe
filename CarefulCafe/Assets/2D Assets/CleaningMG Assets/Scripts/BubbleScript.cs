using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleScript : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private FaucetScript FaucetScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(FaucetScript.faucetPress == true)
        {
            animator.SetBool("BubbleDissolve", true);
        }
    }
}
