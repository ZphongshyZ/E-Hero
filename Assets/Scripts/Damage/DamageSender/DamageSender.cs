using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSender : MyMonobehaviour
{
    //Properties
    [SerializeField] protected float dame = 1;

    //DamageSender System
    public virtual void Send(Transform obj)
    {
        DamageReceiver damageReceiver = obj.GetComponent<DamageReceiver>();
        if (damageReceiver == null) return;
        this.Send(damageReceiver);
    }

    public virtual void Send(DamageReceiver damageReceiver)
    {
        damageReceiver.Deduct(this.dame);
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag != this.transform.tag)
        {
            this.Send(collision.transform);
        }
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag != this.transform.tag)
        {
            this.Send(collision.transform);
        }
    }

    public virtual void SetDamage(float dame)
    {
        this.dame = dame;
    }
}
