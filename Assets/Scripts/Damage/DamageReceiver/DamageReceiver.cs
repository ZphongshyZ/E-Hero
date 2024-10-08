using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DamageReceiver : MyMonobehaviour
{
    //Properties
    [SerializeField] protected float hp = 1;
    [SerializeField] protected int hpMax = 1;
    [SerializeField] protected bool isDead = false;
    public bool _IsDead { get => isDead; set => isDead = value; }

    [SerializeField] protected bool isImmortal = false;
    public bool IsImortal { get => isImmortal; set => isImmortal = value; }

    [SerializeField] protected bool isTakenDamage = false;
    public bool IsTakenDamage { get => isTakenDamage; set => isTakenDamage = value; }

    //DamageReceive System
    protected override void OnEnable()
    {
        base.OnEnable();
        this.Reborn();
    }

    public virtual void Reborn()
    {
        this.hp = this.hpMax;
        isDead = false;
        this.isImmortal = false;
    }

    public virtual void Add(int add)
    {
        if (this.isDead) return;
        this.hp += add;
        if (this.hp > this.hpMax) this.hp = this.hpMax;
    }

    public virtual void Deduct(float deduct)
    {
        this.hp -= deduct;
        if (this.hp < 0) this.hp = 0;
        this.CheckIsDead();
    }

    protected virtual bool IsDead()
    {
        return this.hp <= 0;
    }

    protected virtual void CheckIsDead()
    {
        if (!this.IsDead()) return;
        this.isDead = true;
        this.OnDead();
    }

    protected abstract void OnDead();
}
