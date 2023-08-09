using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class Character : MonoBehaviour,IAttackAble,IDamageAble
{
    
    [SerializeField] protected int currHealth;
    [SerializeField] protected float delayAttack;
    [SerializeField] protected Animator animator;
    protected Character currentEnemy;
    
    public abstract void Die();
    public abstract void DamageSender();
    public int Health => currHealth;
    public virtual void TakeDamage(int _damage)
    {
        currHealth -= _damage;
        GameManager.Instance.CameraController.ShakeCamera();
        
    }



}
