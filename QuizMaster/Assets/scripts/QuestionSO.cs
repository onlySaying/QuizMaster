using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class QuestionSO : ScriptableObject
{
    [TextArea(2,6)]
    [SerializeField]string question = "Enter new question text here";
    [SerializeField]string[] answers = new string[4];
    [SerializeField]int collectAns = -1;

    public string GetQuestion()
    {
        return question;
    }

    public int GetCorrectAnsIdx()
    {
        return collectAns;
    }

    public string getAnswer(int idx)
    {
        return answers[idx];
    }



}
