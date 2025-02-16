using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextAnimatorScript : MonoBehaviour
{
    public Text uiText;
    public float speed = 0.1f;
    // Start is called before the first frame update
    public void DisplayText(string message)
    {
        StartCoroutine(AnimateText(message));
    }

    private IEnumerator AnimateText(string message)
    {
        uiText.text = "";
        foreach (char letter in message.ToCharArray())
        {
            uiText.text += letter;
            yield return new WaitForSeconds(speed);
        }
    }
}
