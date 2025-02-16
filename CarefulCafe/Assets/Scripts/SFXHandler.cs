using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//credit: https://www.youtube.com/watch?v=JnbDxG04i7c
public class SFXHandler : MonoBehaviour
{
    [SerializeField] public AudioSource ovenTimer;
    [SerializeField] public AudioSource cafeMusic;
    [SerializeField] public AudioSource plop;

    public void playTimer()
    {
        ovenTimer.play();
    }



}
