using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UIElements;

public class QuestionController : MonoBehaviour
{
    [SerializeField] Pack selectedPack;
    [SerializeField] QuestionPanel questionPanel;
    [SerializeField] UIController uiController;

    private List<Question> questionsNotAnswered;
    private float startTime;
    private int totalTrueAnswer = 0;
    private int questionAnsweredCounter = 0;
    private int totalAnswer = 0;

    private void Start()
    {
        LoadPack();
    }
    private void LoadPack()
    {
        questionsNotAnswered = selectedPack.listQuestion;
        totalAnswer = questionsNotAnswered.Count;
        startTime = Time.time;
        DisplayRandomQuestion();
    }
    private Question GetRandomQuestion()
    {
        if (questionsNotAnswered.Count == 0) return null;
        int rand = UnityEngine.Random.Range(0, questionsNotAnswered.Count);
        Question result = questionsNotAnswered[rand];
        questionsNotAnswered.Remove(result);
        return result;
    }

    public void DisplayRandomQuestion()
    {

        if (questionAnsweredCounter >= totalAnswer)
        {
            int score = 0;
            if (totalTrueAnswer < totalAnswer)
            {
                score = (totalTrueAnswer % totalAnswer) * 100 + Random.Range(3, 9);
            }
            else
            {
                score = 100 + Random.Range(7, 29);
            }
            uiController.DisplayResult(totalTrueAnswer, totalAnswer, score, GetTotalPlayTime());
        }
        else
        {
            
            Question question = GetRandomQuestion();
            if (question != null)
            {
                questionPanel.DisplayQuestion(questionAnsweredCounter + 1, question);
            }
            questionAnsweredCounter++;
        }

    }

    public void OnQuestionAnswer(bool isTrue)
    {
        questionPanel.DisplayResult();
        if(isTrue)
        {
            totalTrueAnswer++;
        }
    }

    public void OnTimerOut()
    {

        DisplayRandomQuestion();
    }

    public void OnResultDisplayed()
    {
        DisplayRandomQuestion();
    }

    public int GetTotalAnswer()
    { return totalAnswer; }

    public int GetTotalCorrectAnswered()
    {
        return totalTrueAnswer;
    }

    public int GetTotalPlayTime()
    {
        return (int)(Time.time - startTime);
    }
}