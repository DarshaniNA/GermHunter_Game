using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;



[Serializable()]
public struct Answer
{
    //public string Info = string.Empty;
    //public bool IsCorrect = false;

    //public Answer () { }

    [SerializeField] private string _info;
    public string Info { get { return _info; } }

    [SerializeField] private bool _isCorrect;
    public bool IsCorrect { get { return _isCorrect; } }
}

[CreateAssetMenu(fileName = "New Question", menuName = "Quiz/new Question")]
//[Serializable()]
public class Question : ScriptableObject
{
    //public string Info = null;
    //public Answer[] Answers = null;
    //public Boolean UseTimer = false;
    //public int Timer = 0;
    //public AnswerType Type = AnswerType.Single;
    //public Int32 AddScore = 0;

    //public Question () { }

    public enum AnswerType { Multi, Single }

    [SerializeField] private string _info = string.Empty;
    public string Info { get { return _info; } }

    [SerializeField] Answer[] _answers = null;
    public Answer[] Answers { get { return _answers; } }

    //Parameters

    [SerializeField] private bool _useTimer = false;
    public bool UseTimer { get { return _useTimer; } }

    [SerializeField] private int _timer = 0;
    public int Timer { get { return _timer; } }

    [SerializeField] private AnswerType _answerType = AnswerType.Multi;
    public AnswerType GetAnswerType { get { return _answerType; } }

    [SerializeField] private int _addScore = 10;
    public int AddScore { get { return _addScore; } }

    public List<int> GetCorrectAnswers ()
    {
        List<int> CorrectAnswers = new List<int>();
        for (int i = 0; i< Answers.Length; i++)
        {
            if (Answers[i].IsCorrect)
            {
                CorrectAnswers.Add(i); 
            }
        }
        return CorrectAnswers;
    }
}
