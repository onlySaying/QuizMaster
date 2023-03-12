using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Timer : MonoBehaviour
{
    [SerializeField] float timeToCompleteQuestion = 30f;
    [SerializeField] float timeToShowCorrectAnswer = 10f;
    float timerValue;
    public bool isAnweringQuestion;
    public float fillFraction;
    public bool loadNextQuestion;


    void Update()
    {
        updateTimer();
       
    }

    void updateTimer()
    {  
        timerValue -= Time.deltaTime;
        if(isAnweringQuestion == true)
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            if(timerValue <= 0)
            {
                isAnweringQuestion = false;
                timerValue = timeToShowCorrectAnswer;
            }
        }
        else
        {
            if(timerValue > 0)
            {
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            if(timerValue <=0)
            {
                timerValue = timeToCompleteQuestion;
                isAnweringQuestion = true;
                loadNextQuestion = true;
            }
        }

        Debug.Log(isAnweringQuestion + " : " + timerValue + " = " + fillFraction);
    }

    public void cancelTimer()
    {
        timerValue = 0;
    }

}
