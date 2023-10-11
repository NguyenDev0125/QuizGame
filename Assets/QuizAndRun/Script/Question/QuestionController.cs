using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class QuestionController : MonoBehaviour
{
    [SerializeField] Pack questionPack;
    [SerializeField] QuestionPanel questionPanel;
    [SerializeField] UIController uiController;
    
    private List<Question> questionsNotAnswered;
    private float startTime;
    private int totalTrueAnswer = 0;
    private int questionAnsweredCounter = 0;
    private int totalAnswer = 0;
    private void Awake()
    {
        questionsNotAnswered = questionPack.listQuestion;
    }
    private void Start()
    {
        LoadPack();
    }
    private void LoadPack()
    {
        totalAnswer = questionsNotAnswered.Count;
        startTime = Time.time;
        DisplayRandomQuestion();
    }
    private Question GetRandomQuestion()
    {
        if(questionsNotAnswered.Count == 0) return null;
        int rand = UnityEngine.Random.Range(0,questionsNotAnswered.Count);
        Question result = questionsNotAnswered[rand];
        questionsNotAnswered.Remove(result);
        return result;
    }

    public void DisplayRandomQuestion()
    {
        Question question = GetRandomQuestion();
        if (question != null)
        {
            questionPanel.DisplayQuestion(questionAnsweredCounter+1, question);
        }
    }

    public void OnQuestionAnswer(bool isTrue)
    {

    }

    public void OnTimerOut()
    {
        questionAnsweredCounter++;
        DisplayRandomQuestion();
    }

    public void OnResultDisplayed()
    {
        DisplayRandomQuestion();
        if (questionsNotAnswered.Count == 0)
        {
            uiController.DisplayResult();
        }
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