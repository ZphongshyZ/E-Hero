using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Skeleton : Monster, IDFirstAttack
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
