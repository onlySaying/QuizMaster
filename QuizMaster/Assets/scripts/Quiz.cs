using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [Header("Question")]
    QuestionSO currentQuestion;
    [SerializeField] List<QuestionSO> questions = new List<QuestionSO>();
    [SerializeField]TextMeshProUGUI questionText;

    [Header("Answer")]
    [SerializeField]GameObject[] answerButton;
    int correctAnswerIndex;
    bool hasAnsweredEarly = true;

    [Header("Button Color")]
    [SerializeField]Sprite defaultAnswerSprite;
    [SerializeField]Sprite correctAnswerSprite;
    
    [Header("Timer")]
    [SerializeField] Image timerImage;
    Timer timer;

    [Header("Scoring")]
    [SerializeField] TextMeshProUGUI scoreText;
    ScoreKeeper scoreKeeper;

    [Header("ProgressBar")]
    public bool isComplete= false;
    [SerializeField]Slider progressBar;


    void Awake()
    {
        timer = FindObjectOfType<Timer>();
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
        progressBar.value = 0;
        progressBar.maxValue = questions.Count;
    }

    void Update()
    {
        timerImage.fillAmount = timer.fillFraction;
         if(timer.loadNextQuestion == true)
        {
            if(progressBar.value == progressBar.maxValue)
            {isComplete = true;return;}
            hasAnsweredEarly = false;
            getNextQuestion();
            timer.loadNextQuestion = false;
        }
        else if(!hasAnsweredEarly && !timer.isAnweringQuestion)
        {
            DisplayAnswer(-1);
            setButtonState(false);
        }
    }
    
    public void OnAnswerSelected(int idx)
    {
        hasAnsweredEarly = true;
        correctAnswerIndex = currentQuestion.GetCorrectAnsIdx();
        DisplayAnswer(idx);
        setButtonState(false);
        timer.cancelTimer();
        scoreText.text = "Score : " +  scoreKeeper.calculateScore() + "%";
       
    }

    void DisplayAnswer(int idx)
    {
        Image buttonImage;
        correctAnswerIndex = currentQuestion.GetCorrectAnsIdx();
        if(idx == correctAnswerIndex)
        {
            questionText.text = "Correct!";
            buttonImage = answerButton[idx].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
            scoreKeeper.incrementCorrectAnswers();
        }
        else
        {
            string text = "Sorry, U r incorrect. Correct answer was\n";
            int index = correctAnswerIndex + 1;
            text += index;
            questionText.text = text;
            buttonImage = answerButton[correctAnswerIndex].GetComponent<Image>();
            buttonImage.sprite = correctAnswerSprite;
        }
    }

    void getNextQuestion()
    {
        if(questions.Count > 0)
        {
            setButtonState(true);
            setDefaultButtonSprites();
            getRandomQuestion();
            DisplayQuestion();
            scoreKeeper.incrementQuestionSeen();
            progressBar.value++;
           
        }
    }
    
    void getRandomQuestion()
    {
        int index = Random.Range(0, questions.Count);
        currentQuestion = questions[index];
        if(questions.Contains(currentQuestion))
        {
            questions.Remove(currentQuestion);
        }
    }

    void DisplayQuestion()
    {
        questionText.text = currentQuestion.GetQuestion();
        for(int i = 0; i < answerButton.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButton[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.getAnswer(i);
        }
    }

    void setButtonState(bool state)
    {
       for(int i = 0; i <answerButton.Length; i++)
       {
            Button button = answerButton[i].GetComponent<Button>();
            button.interactable = state;
       }
    }

    void setDefaultButtonSprites()
    {
        Image buttonImage;
        for(int i = 0; i<answerButton.Length; i++)
        {
            buttonImage = answerButton[i].GetComponent<Image>();
            buttonImage.sprite = defaultAnswerSprite;
        }
    }
}
