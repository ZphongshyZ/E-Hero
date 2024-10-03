using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MyMonobehaviour
{
    [SerializeField] protected bool isAttacking = false;
    public bool IsAttacking { get => isAttacking;}

    [SerializeField] protected bool isActiving = false;
    public bool IsActiving { get =>  isActiving; set {  isActiving = value; } }

    [SerializeField] protected float dame;

    [SerializeField] protected Vector2 direction = Vector2.right;

    [SerializeField] public CharacterSO characterSO;

    [SerializeField] protected Animator animator;

    protected override void LoadComponents()
    {
        this.animator = GetComponent<Animator>();
        this.LoadSO();
        this.dame = this.characterSO.dame;
    }

    protected virtual void LoadSO()
    {
        if (this.characterSO != null) return;
        string resPath = "Character/" + transform.name;
        this.characterSO = Resources.Load<CharacterSO>(resPath);
        Debug.Log(transform.name + " CharacterSO " + resPath, gameObject);
    }

    public virtual void StartAttack()
    {
        this.isAttacking = true;
    }

    public virtual void FinishAttack()
    {
        this.isAttacking = false;
    }

    public virtual void StartActive()
    {
        this.isActiving = true;
    }

    public virtual void FinishActive()
    {
        if (!this.isActiving) return;
        this.isActiving = false;
    }

    public void setDirection(Vector2 vector)
    {
        this.direction = vector;
    }
}
