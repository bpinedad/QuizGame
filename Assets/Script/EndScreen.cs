using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreen : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI headerText;
    [SerializeField] TextMeshProUGUI buttonText;
    ScoreKeeper scoreKeeper;

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void ShowFinalScore(){
        headerText.text = $"Ah perro!! Nuevo Record\nScore {scoreKeeper.CalculateScore()}";
    }

    public void ShowWelcomeMessage(){
        headerText.text = $"Bienvenida al mejor juego de la historia!!";
    }

    public void SetButtonReplay(){
        buttonText.text = $"Juegar de nuevo";
    }

    public void SetButtonStart(){
        buttonText.text = $"Comenzar";
    }
}
