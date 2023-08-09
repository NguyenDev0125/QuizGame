using System.Collections;
using UnityEngine;

public class EnemyController : Character
{
    [SerializeField] protected VoidEventChanel OnEnemyDie;
    private void OnEnable()
    {
        currHealth = Random.Range(10, 15);
    }
    public override void Die()
    {
        OnEnemyDie.Raise();
        animator.SetTrigger(AnimatorTriggerKey.T_ENEMY_DIE);
    }

    public override void TakeDamage(int _damage)
    {
        
        base.TakeDamage(_damage);
        if(currHealth <= 0)
        {
            Die();
        }
        else
        {
            animator.SetTrigger(AnimatorTriggerKey.T_ENEMY_HURT);
        }
        
        SoundManager.Instance.Play("Hit");
        
    }
    public void StartCombat(PlayerController _enemy)
    {
        int _enemyHealth = _enemy.Health;
        currentEnemy = _enemy;
        StartCoroutine(Attack(_enemyHealth));
    }

    private IEnumerator Attack(int _count)
    {
        yield return new WaitForSeconds(1f);
        for(int i = 0; i < _count; i++)
        {
            yield return new WaitForSeconds(delayAttack);
            animator.SetTrigger(AnimatorTriggerKey.T_ENEMY_ATTACK);
            SoundManager.Instance.Play("Sword");
        }
    }

    public override void DamageSender()
    {
        currentEnemy.TakeDamage(1);
    }


}
