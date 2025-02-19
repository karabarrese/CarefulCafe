using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScene : MonoBehaviour
{
    [SerializeField] private Image intro;
    [SerializeField] private Button btn;
    
    public void HideIntro(){
        intro.gameObject.SetActive(false);
    }
}
