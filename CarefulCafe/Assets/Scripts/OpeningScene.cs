using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpeningScene : MonoBehaviour
{
    [SerializeField] private Canvas intro;
    
    public void HideIntro(){
        intro.gameObject.SetActive(false);
    }
}
