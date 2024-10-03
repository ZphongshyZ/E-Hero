using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MyMonobehaviour
{
    [SerializeField] protected Character character;

    protected override void LoadComponents()
    {
        this.character = GetComponent<Character>();
    }

    protected void OnFirstAttack()
    {
        if (this.character.IsAttacking || this.character.IsActiving) return;
        IDFirstAttack character = this.character.GetComponent<IDFirstAttack>();
        if (character == null) return;
        character.FirstAttack();
    }

    protected void OnCharging()
    {
        if (this.character.IsAttacking) return;
        IDCharging character = this.character.GetComponent<IDCharging>();
        if (character == null) return;
        character.Charging();
    }

    protected void OnSecondAttack()
    {
        if (this.character.IsAttacking) return;
        IDSecondAttack character = this.character.GetComponent<IDSecondAttack>();
        if (character == null) return;
        character.SecondAttack();
    }

    protected void OnThirdAttack()
    {
        if (this.character.IsAttacking) return;
        IDThirdAttack character = this.character.GetComponent<IDThirdAttack>();
        if (character == null) return;
        character.ThirdAttack();
    }
}
