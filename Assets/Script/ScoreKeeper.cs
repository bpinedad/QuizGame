using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    Quiz quiz;
    int correctAnswers = 0;
    int questionsSeen  = 0;

    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
    }

    public int GetCorrectAnswers(){
        return correctAnswers;
    }

    public void IncrementCorrectAnswers(){
        correctAnswers++;
    }

    public int GetQuestionsSeen(){
        return questionsSeen;
    }

    public void IncrementQuestionsSeen(){
        questionsSeen++;
    }

    public int CalculateScore(){
        return Mathf.RoundToInt(((float)correctAnswers/quiz.totalQuestionsCount) * 100);
    }
}
