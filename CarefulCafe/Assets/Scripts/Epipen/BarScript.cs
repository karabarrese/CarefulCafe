using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarScript : MonoBehaviour
{
    public float height;
    // Start is called before the first frame update
    public float GetHeight()
    {
        height = transform.localScale.y;
        Debug.Log("Height of the bar is: " + height);
        return height;
    }
}
