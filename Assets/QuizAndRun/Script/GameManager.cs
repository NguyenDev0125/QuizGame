using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region MAKE SINGLETON
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }




    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        StartingGame();
    }
    #endregion
    #region Propeties
    public QuestionPanel QuestionPanel { get => questionPanel; }
    public EnemySpawnner EnemySpawnner { get => enemySpawnner; }
    public PlayerController PlayerController { get => playerController; }
    #endregion

    [SerializeField] QuestionPanel questionPanel;
    [SerializeField] PlayerController playerController;
    [SerializeField] QuestionController questionController;
    [SerializeField] EnemySpawnner enemySpawnner;
    [SerializeField] CombatController combbatController;
    [SerializeField] QuestionPackManager questionPackManager;

    [SerializeField] VoidEventChanel OnPlayerMoveComplete;
    [SerializeField] BoolEventChanel OnAnswerButtonClick;
    [SerializeField] VoidEventChanel OnPlayerDie;
    [SerializeField] VoidEventChanel OnEnemyDie;
    [SerializeField] VoidEventChanel OnTimeOut;

    private EnemyController enemyController;

    private void OnEnable()
    {
        if(OnPlayerMoveComplete) OnPlayerMoveComplete.OnEventRaised += PlayerMoveComplete;
        if (OnAnswerButtonClick) OnAnswerButtonClick.OnEventRaised += AnswerButtonClick;
        if (OnPlayerDie) OnPlayerDie.OnEventRaised += PlayerDie;
        if(OnEnemyDie) OnEnemyDie.OnEventRaised += EnemyDie;
        if (OnTimeOut) OnTimeOut.OnEventRaised += TimeOut;

    }
    private void StartingGame()
    {
        
        questionController.Init();
        SpawnEnemy();
        SoundManager.Instance.Play(SoundName.Background);


    }
    private void SpawnEnemy()
    {
        enemyController = enemySpawnner.SpawnEnemy();
        playerController.SetNextPoint(enemyController.gameObject.transform.position);
    }
    private void PlayerMoveComplete()
    {
        questionController.DisplayRandomQuestion();
    }

    private void AnswerButtonClick(bool _isTrue)
    {
        Debug.Log("Result : "+ _isTrue);
        questionPanel.ShowResult();
        combbatController.SetCombat(playerController, enemyController);
        combbatController.StartCombat(_isTrue);

    }

    private void PlayerDie()
    {
        GameOver();
    }

    private void EnemyDie()
    {
        if(!IsVictory())
        {
            SpawnEnemy();
        }
        else
        {
            Victory();
        }

    }
    private bool IsVictory()
    {
        return questionController.IsAnsweredAllQuestion;
    }

    private void Victory()
    {
        Debug.Log("Victory");
    }

    private void TimeOut()
    {
        Debug.Log("Timeout");
    }

    private void GameOver()
    {
        Debug.Log("GameOver");
    }





}
