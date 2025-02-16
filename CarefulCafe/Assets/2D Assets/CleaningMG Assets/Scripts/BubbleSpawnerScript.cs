using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSpawnerScript : MonoBehaviour
{
    public int bubbleCount;
    public int bubbleMax;
    public GameObject Bubble;
    public float spawnRate;
    private float timer = 0;
    Vector3 mousePositionOffset;
    [SerializeField] private WhiskScript WhiskScript;
    public GameObject Whisk;
    public bool maxBubblesReached;
    public GameObject[] bubbleArr;
    private Vector3 GetMouseWorldPosition()
    {
        //capture mouse position & return world point
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Start is called before the first frame update
    void Start()
    {
        bubbleArr = new GameObject[bubbleMax]; // array of bubble GameObjects
    }

    // Update is called once per frame
    void Update()
    {
        if(WhiskScript.spongeHover == true && WhiskScript.spongeHoverStay == true)
        {
            if (timer < spawnRate)
            {
                timer = timer + Time.deltaTime;
            }
            else
            {
                if (bubbleCount <= bubbleMax)
                {
                    spawnBubble();
                    bubbleCount++;
                }
                // Debug.Log(bubbleCount);
                // Debug.Log(GetMouseWorldPosition().ToString());
                timer = 0;
            }
        }
        maxBubblesReached = bubbleCount >= bubbleMax;
    }

    void spawnBubble()
    {
        GameObject myBubble = Instantiate(Bubble, new Vector3(GetMouseWorldPosition().x, GetMouseWorldPosition().y, (float)(-0.2)), transform.rotation);
        myBubble.transform.SetParent(Whisk.transform); // instantiate bubble as a child of whisk
    }
}
