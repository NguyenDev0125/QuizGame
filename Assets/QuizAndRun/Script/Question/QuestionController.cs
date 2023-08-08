using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class QuestionController : MonoBehaviour
{
    [SerializeField] private string questionTitle;
    [SerializeField] private QuestionPack questionPack;
    
    private List<QuestionData> questionsNotAnswered;
    
    private int currentQuestionIndex = 1;
    public bool IsAnsweredAllQuestion
    {
        get => questionsNotAnswered.Count == 0;
    }
    public void Init()
    {
        LoadQuestions();
    }

    private void LoadQuestions()
    {
        questionsNotAnswered = questionPack.QuestionDatas.ToList();
        Debug.Log($"QuestionManager.LoadQuestion() : {questionsNotAnswered.Count} question loaded");
    }

    private QuestionData GetRandomQuestion()
    {
        if(questionsNotAnswered.Count == 0) return null;
        int rand = UnityEngine.Random.Range(0,questionsNotAnswered.Count);
        QuestionData result = questionsNotAnswered[rand];
        questionsNotAnswered.Remove(result);
        return result;
    }

    public void DisplayRandomQuestion()
    {
        QuestionData question = GetRandomQuestion();
        if (question != null)
        {
            string title = $"Question {currentQuestionIndex++}";
            GameManager.Instance.QuestionPanel.ShowQuestion(title, question);
        }
        
    }






}
