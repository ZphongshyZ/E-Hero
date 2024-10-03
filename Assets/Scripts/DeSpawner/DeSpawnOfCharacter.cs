using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeSpawnOfCharacter : DeSpawnByTime
{
    [SerializeField] protected DamageReceiver damageReceiver;

    protected override void LoadComponents()
    {
        this.damageReceiver = transform.parent.GetComponentInChildren<DamageReceiver>();
    }

    protected override bool CanDeSpawn()
    {
        if (!damageReceiver._IsDead) return false;
        return base.CanDeSpawn();
    }

    public override void DeSpawnObj()
    {
        if (this.transform.parent.tag == "Player") return;
        base.DeSpawnObj();
    }
}
