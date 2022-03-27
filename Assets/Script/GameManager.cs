using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] AudioClip startAudio;
    [SerializeField] AudioClip finishAudio;
    Quiz quiz;
    EndScreen endScreen;
    bool start =true;

    // Start is called before the first frame update
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();

    }

    void Start(){
        quiz.gameObject.SetActive(false);
        endScreen.gameObject.SetActive(true);
        endScreen.SetButtonStart();
        endScreen.ShowWelcomeMessage();    
        GetComponent<AudioSource>().PlayOneShot(startAudio);
    }

    // Update is called once per frame
    void Update()
    {
        if (quiz.isComplete)
        {
            quiz.gameObject.SetActive(false);
            endScreen.gameObject.SetActive(true);
            endScreen.ShowFinalScore();
            endScreen.SetButtonReplay();
            GetComponent<AudioSource>().PlayOneShot(finishAudio);
            quiz.isComplete = false;
        }
    }

    public void OnReplay(){
        if (start) {
            quiz.gameObject.SetActive(true);
            endScreen.gameObject.SetActive(false);
            quiz.timer.timerValue = quiz.timer.timeToAnswer;
            start = false;
        } else {
            SceneManager.LoadScene("Game");
        }
    }
}
