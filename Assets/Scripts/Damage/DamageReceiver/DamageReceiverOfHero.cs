using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiverOfHero : DamageReceiverOfCharacter
{
    public IEnumerator DamageReceived(float time)
    {
        this.isImmortal = true;
        this.rb.velocity = Vector3.zero;
        this.rb.isKinematic = true;
        this.isTakenDamage = true;
        gameObject.layer = LayerMask.NameToLayer("TakenDamage");
        yield return new WaitForSeconds(time);
        this.isImmortal = false;
        this.rb.isKinematic = false;
        this.isTakenDamage = false;
        this.gameObject.layer = originalLayer;
    }

    protected override void CheckIsDead()
    {
        if(!this.isDead)
        {
            StartCoroutine(DamageReceived(0.5f));
            return;
        }
        else
        {
            this.isDead = true;
            this.animator.SetTrigger("isDeath");
            this.OnDead();
        }
    }
}
