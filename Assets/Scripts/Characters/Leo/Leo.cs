using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Leo : Character, IDFirstAttack, IDCharging, IDSecondAttack, IDThirdAttack
{
    [SerializeField] protected float maxChargeTime = 3f;
    [SerializeField] protected float maxSize = 2f;
    [SerializeField] protected float chargeTime = 0f;
    [SerializeField] protected int maxDame = 5;

    [SerializeField] protected bool isCharging = false;

    [SerializeField] protected Transform firePoint;
    [SerializeField] protected Transform firePoint_2;
    [SerializeField] protected GameObject energy;
    [SerializeField] protected Bullet bullet_1;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.firePoint = GameObject.Find("FirePoint").transform;
        this.firePoint_2 = this.transform.GetComponentInChildren<SpawnAttack>().transform;
    }

    protected void FixedUpdate()
    {
        this.bullet_1.SetDirection(this.direction);
        if (this.isCharging)
        {
            this.chargeTime += Time.fixedDeltaTime;
            this.chargeTime = Mathf.Min(chargeTime, maxChargeTime);
        }
    }

    public void FirstAttack()
    {
        this.animator.SetTrigger("Atk1");
    }

    public void StartCharging()
    {
        this.energy.SetActive(true);
        isCharging = true;
    }

    public void Charging()
    {
        this.StartCharging();
    }

    public void Shoot()
    {
        this.isCharging = false;
        float chargePercent = this.chargeTime / this.maxChargeTime;
        float projectileSize = Mathf.Lerp(1f, this.maxSize, chargePercent);
        Transform newBullet = BulletSpawner.Instance.Spawn("Bullet_1", this.firePoint.position, this.firePoint.rotation);
        if (newBullet.GetComponent<DamageSender>() == null) return;
        float dameBullet = Mathf.Lerp(this.dame, this.maxDame, chargePercent);
        newBullet.GetComponent<DamageSender>().SetDamage(dameBullet);
        newBullet.transform.localScale *= projectileSize;
        newBullet.gameObject.SetActive(true);
        this.chargeTime = 0f;
    }

    public void SecondAttack()
    {
        this.animator.SetTrigger("Atk2");
    }

    public void RainArrows()
    {
        Transform newBullet = BulletSpawner.Instance.Spawn("Bullet_2", this.firePoint_2.position, this.firePoint_2.rotation);
        if (newBullet.GetComponent<DamageSender>() == null) return;
        newBullet.gameObject.SetActive(true);
    }

    public void ThirdAttack()
    {
        this.animator.SetTrigger("Atk3");
    }    

    public override void FinishAttack()
    {
        base.FinishAttack();
        this.energy.SetActive(false);
        this.isAttacking = false;
    }
}
