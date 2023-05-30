using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TMP_Text playingTime;
    private float startTime;
    [SerializeField] private bool timerOn;
    
    void Start()
    {
        timerOn = true;
        startTime = Time.time;
    }

    
    void Update()
    {   if(timerOn){
            float runTime = Time.time - startTime;
            int hours = (int) runTime/3600;
            int minutes = (int) (runTime%3600)/60;
            int seconds = (int) runTime%60;
            playingTime.text = string.Format("{0:0} : {1:00} : {2:00}", hours ,minutes, seconds);
        }
        else return;
    }

    public void Stop(){
        timerOn = false;
        playingTime.color = Color.green;
    }
}

