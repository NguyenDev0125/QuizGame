using System.Linq;
using UnityEngine;

public class PlayerManager : Character, IDamageAble, IAttackAble
{

    public override void DamageSender()
    {

    }

    public override void TakeDamage(int _damage)
    {
        base.TakeDamage(_damage);
    }

    public virtual bool CheckLastHit(int _damage)
    {
        return currHealth - _damage <= 0;
    }

    public override void Die()
    {

    }

}
