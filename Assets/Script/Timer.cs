using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] public float timeToAnswer = 7f;
    [SerializeField] float timeToShowCorrect = 3f;

    public bool loadNextQuestion;
    public float fillFraction;

    public bool isAnswering;
    public float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    public void CancelTimer(){
        timerValue = 0;
    }

    void UpdateTimer(){
        timerValue -= Time.deltaTime;

        if (isAnswering){
            if(timerValue > 0) {
                fillFraction = timerValue / timeToAnswer;
            } else {
                isAnswering = false;
                timerValue = timeToShowCorrect;
            }
        } else {
            if(timerValue > 0) {
                fillFraction = timerValue / timeToShowCorrect;
            } else {
                isAnswering = true;
                timerValue = timeToAnswer;
                loadNextQuestion = true;
            }
        }
    }
}
