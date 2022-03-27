using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Question", menuName = "Quiz Question", order = 0)]
public class QuestionSO : ScriptableObject {
    [TextArea(2,6)] // min and max lines intext field 
    [SerializeField] string question = "Enter new Question";
    [SerializeField] string[] answers = new string[4];
    [SerializeField] int correctAnswer;

    public string GetQuestion()
    {
        return question;
    }

    public string GetAnswer(int index)
    {
        return answers[index];
    }

    public int GetCorrectAnswer()
    {
        return correctAnswer;
    }

}