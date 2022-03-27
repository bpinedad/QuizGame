using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    [Header("Questions")]
    [SerializeField] TextMeshProUGUI questionText;
    [SerializeField] List<QuestionSO> questions;
    public int totalQuestionsCount;
    QuestionSO currentQuestion;
   
    [Header("Answers")]
    [SerializeField] GameObject[] answerButtons;
    bool answeredOnTime = true;

    [Header("Button Colors")]
    [SerializeField] Sprite defaultAnswerSprite;
    [SerializeField] Sprite correctAnswerSprite;
    Color32 correctAnswerColor = new Color32(0, 255, 0, 255);
    Color32 wrongAnswerColor = new Color32(255, 0, 0, 255);
    Color32 defaultAnswerColor = new Color32(255, 255, 255, 255);

    [Header("Timer")]
    [SerializeField] Image timerImage;
    public Timer timer;

    [Header("Score")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("Slider")]
    [SerializeField] Slider progressBar;

    public bool isComplete;

    void Awake()
    {
        totalQuestionsCount = questions.Count;
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.maxValue = totalQuestionsCount;
        progressBar.value = 0;
        scoreText.text = $"Score: {scoreKeeper.CalculateScore()}";
    }

    void Update(){
        timerImage.fillAmount = timer.fillFraction;

        if (timer.loadNextQuestion){
            if (progressBar.value == progressBar.maxValue){
                isComplete = true;
                return;  
            }

            answeredOnTime = false;
            GetNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if (!answeredOnTime && !timer.isAnswering){
            DisplayAnswer(-1); //to ensure no correct answer matches
            SetButtonState(false);
            answeredOnTime = true;
        }
    }

    public void OnAnswerSelected(int index){
        answeredOnTime = true;
        DisplayAnswer(index);
        SetButtonState(false);
        timer.CancelTimer();
        scoreText.text = $"Score: {scoreKeeper.CalculateScore()}";
    }

    void DisplayAnswer(int index){
        if (index == currentQuestion.GetCorrectAnswer()){
            questionText.text = "Correcto!";
            Image buttonImage = answerButtons[index].GetComponent<Image>();
            buttonImage.color = correctAnswerColor; 
            scoreKeeper.IncrementCorrectAnswers();
        }
        else {
            int correctAnswerIndex = currentQuestion.GetCorrectAnswer();
            string correctAnswerText = currentQuestion.GetAnswer(correctAnswerIndex);
            questionText.text = $"La cagates! La respuesta era :\n{correctAnswerText}";
            
            //Mark orange failed correct one
            Image buttonImage = answerButtons[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite; 

            //Mark in red wrong answer
            if (index >= 0) {
                buttonImage = answerButtons[index].GetComponent<Image>();
                buttonImage.color = wrongAnswerColor; 
            }
        }

        progressBar.value++;
    }

    void GetNextQuestion(){
        if (questions.Count > 0) {
            SetButtonState(true);
            SetDefaultButtonSprite();
            GetRandomQuestion();
            DisplayQuestion();
            scoreKeeper.IncrementQuestionsSeen();
        }
    }

    void GetRandomQuestion(){
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];

        if (questions.Contains(currentQuestion)){
            questions.Remove(currentQuestion);
        }       
    }

    void DisplayQuestion(){
        questionText.text = currentQuestion.GetQuestion();

        for (int i= 0; i<answerButtons.Length ; i++) {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.GetAnswer(i);
        }
        SetButtonState(true);
    }

    void SetButtonState (bool state){
        for (int i= 0; i<answerButtons.Length ; i++) {
            Button button = answerButtons[i].GetComponent<Button>();
            button.interactable = state;
        }
    }

    void SetDefaultButtonSprite(){
        for (int i= 0; i<answerButtons.Length ; i++) {
            Image buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.color = defaultAnswerColor; 

            buttonImage = answerButtons[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite; 
        }
    }
}
