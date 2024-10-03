using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageReceiverOfCharacter : DamageReceiver
{
    [SerializeField] protected Animator animator;
    [SerializeField] protected Character character;
    [SerializeField] protected DamageReceiver dameReceiver;
    [SerializeField] protected DeSpawner deSpawner;
    [SerializeField] protected CapsuleCollider2D body;
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected int originalLayer;

    protected override void LoadComponents()
    {
        this.originalLayer = this.gameObject.layer;
        this.animator = this.transform.GetComponent<Animator>();
        this.character = this.transform.GetComponent<Character>();
        this.deSpawner = transform.GetComponentInChildren<DeSpawner>();
        this.dameReceiver = transform.GetComponent<DamageReceiver>();
        this.body = this.transform.GetComponent<CapsuleCollider2D>();
        this.rb = this.transform.GetComponent<Rigidbody2D>();
    }

    public override void Deduct(float deduct)
    {
        if (this.isDead || this.isImmortal) return;
        this.animator.SetTrigger("TakenDamage");
        base.Deduct(deduct);
    }

    public override void Reborn()
    {
        this.hpMax = this.character.characterSO.hpMax;
        base.Reborn();
        this.isImmortal = this.character.characterSO.isImmortal;
        this.gameObject.layer = originalLayer;
    }

    protected override void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.animator.SetTrigger("isDeath");
        this.OnDead();
    }

    protected override void OnDead()
    {
        this.gameObject.layer = LayerMask.NameToLayer("Dead");
    }
}
