using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goblin : Monster, IDFirstAttack
{
    protected override void Attack()
    {
        this.FirstAttack();
    }

    public void FirstAttack()
    {
        this.animator.SetTrigger("Atk1");
    }
}
