using System;
using System.Collections;
using UnityEngine;

public class PlayerController : Character
{
    [Header("Reference")]
    [SerializeField] Rigidbody2D rigidbody;

    [Header("Values")]
    [SerializeField] float playerMoveSpeed;
    [SerializeField] float distPlayerToEnemy;
    [SerializeField] float delayMove;
    float delayTimer;
    
    [Header("Event listener")]
    [SerializeField] VoidEventChanel OnPlayerMoveComplete;
    [SerializeField] protected VoidEventChanel OnPlayerDie;
    private Vector3 nextPoint;
    private bool canMove = false;
    
    

    private void Update()
    {
        MoveToNextPoint();
    }

    private void MoveToNextPoint()
    {
        if(canMove)
        {
            if(delayTimer > 0)
            {
                delayTimer -= Time.deltaTime;
                if(delayTimer < -999f) delayTimer = 0;
                return;
            }
            if(transform.position.x + distPlayerToEnemy < nextPoint.x)
            {
                float moveDir = nextPoint.x - transform.position.x;
                float rawDir = moveDir / Math.Abs(moveDir);

                rigidbody.velocity = new Vector2(rawDir * playerMoveSpeed, rigidbody.velocity.y);
                animator.SetBool(AnimatorTriggerKey.B_PLAYER_RUN , true);
                SoundManager.Instance.Play(SoundName.FootStepGrass);
            }
            else
            {
                canMove = false;
                rigidbody.velocity = Vector3.zero;
                animator.SetBool(AnimatorTriggerKey.B_PLAYER_RUN, false);
                SoundManager.Instance.Stop(SoundName.FootStepGrass);
                OnMoveComplete();
            }
        }
    }

    public void SetNextPoint(Vector3 _nextPoint)
    {
        nextPoint.x = _nextPoint.x;
        delayTimer = delayMove;
        canMove = true;
    }

    private void OnMoveComplete()
    {
        OnPlayerMoveComplete.Raise();
    }


    public void StartCombat(EnemyController _enemy)
    {
        currentEnemy = _enemy;
        int enemyHealth = _enemy.Health;
        Debug.Log("Start combat enemy : "+ enemyHealth);
        StartCoroutine(ComboAttack(enemyHealth));
    }

    private IEnumerator ComboAttack(int _count)
    {

        int attack = 1;
        int Qcount = 1;
        for(int i = 0; i < _count - 1; i++)
        {
            yield return new WaitForSeconds(delayAttack);
            switch (attack)
            {
                case 1: animator.SetTrigger(AnimatorTriggerKey.T_PLAYER_ATTACK_1); SoundManager.Instance.Play(SoundName.YasuoCast1); break;
                case 2: animator.SetTrigger(AnimatorTriggerKey.T_PLAYER_ATTACK_2); SoundManager.Instance.Play(SoundName.YasuoCast2); break;
                case 3: animator.SetTrigger(AnimatorTriggerKey.T_PLAYER_ATTACK_3);
                    Qcount++;
                    if (Qcount == 3)
                    {
                        Qcount = 0;
                        SoundManager.Instance.Play(SoundName.YasuoQ3);

                    }
                    else
                    {
                        SoundManager.Instance.Play(SoundName.YasuoQ2);
                    }

                    break;
                case 4: animator.SetTrigger(AnimatorTriggerKey.T_PLAYER_ATTACK_4); SoundManager.Instance.Play(SoundName.YasuoCast3); break;
                default: animator.SetTrigger(AnimatorTriggerKey.T_PLAYER_ATTACK_1); SoundManager.Instance.Play(SoundName.YasuoCast3); break;
            }
            if (attack >= 4)
            {
                attack = 0;
            }
            else
            {
                attack++;
            }

            
            
        }
        yield return new WaitForSeconds(delayAttack + 1);
        LastHitAttack();
    }

    private void LastHitAttack()
    {
        SoundManager.Instance.Play(SoundName.YasuoR);
        transform.position = currentEnemy.transform.position + new Vector3(0f, 1f, 0f);
        animator.SetTrigger(AnimatorTriggerKey.T_PLAYER_AIR_ATTACK);
    }

    public override void DamageSender()
    {
        if(currentEnemy == null) return;
        currentEnemy.TakeDamage(1);
    }
    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
        animator.SetTrigger(AnimatorTriggerKey.T_PLAYER_HURT);
    }
    public override void Die()
    {
        OnPlayerDie.Raise();
        animator.SetTrigger(AnimatorTriggerKey.T_PLAYER_DIE);
        SoundManager.Instance.Play(SoundName.YasuoDeath);
        Debug.Log(AnimatorTriggerKey.T_PLAYER_DIE);
    }
}
