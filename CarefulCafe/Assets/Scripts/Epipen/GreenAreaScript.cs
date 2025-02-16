using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenAreaScript : MonoBehaviour
{
    public ArrowScript arrow;
    public GameObject epipen;
    private bool epipenInstantiated = false;
    // Start is called before the first frame update
    void Start()
    {
        arrow = GameObject.FindGameObjectWithTag("Arrow").GetComponent<ArrowScript>();
        if (arrow == null || arrow.rigid == null)
        {
            Debug.LogError("Arrow or its Rigidbody2D component is not found!");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!epipenInstantiated && arrow.rigid.velocity.magnitude < 0.01f && collision.gameObject.layer == 3)
        {
            Debug.Log("Arrow is in the green area");
            epipen.SetActive(true);
            // Instantiate(epipen, new Vector3(-2, 2, 0), Quaternion.identity);
            epipenInstantiated = true;
        }
    }
}
