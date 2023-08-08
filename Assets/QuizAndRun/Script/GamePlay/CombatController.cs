using UnityEngine;

public class CombatController : MonoBehaviour
{
     PlayerController playerController;
     EnemyController enemyController;

    public void SetCombat(PlayerController _player , EnemyController _enemy)
    {
        playerController = _player;
        enemyController = _enemy;
    }

    public void StartCombat(bool _isPlayerWin)
    {
        if(_isPlayerWin)
        {
            playerController.StartCombat(enemyController);
        }else
        {
            enemyController.StartCombat(playerController);
        }
    }
}
