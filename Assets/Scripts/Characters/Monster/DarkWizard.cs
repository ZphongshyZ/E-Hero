using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkWizard : Monster, IDFirstAttack, IDSecondAttack
{
    [SerializeField] protected Transform firePoint;

    protected override void ResetValue()
    {
        this.isFacingRight = false;
    }

    protected override void Update()
    {
        Invoke("Patrol", 5f);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
    }

    protected override void Patrol()
    {
        if (this.isDead || isAttacking)
        {
            this.rb.velocity = Vector2.zero;
            return;
        }
        if (patrolPoints.Length == 0) return;
        this.animator.SetBool("isRunning", true);
        Transform targetPoint = patrolPoints[this.currentPatrolPoint];
        Vector2 direction = (targetPoint.position - transform.position).normalized;
        this.rb.velocity = new Vector2(direction.x * this.speed, 0);
        float distanceFromPlayer = this.transform.position.x - this.player.position.x;
        if (direction.x > 0 && !this.isFacingRight && !this.isAttacking)
        {
            Flip();
            this.SecondAttack();
        }
        else if (direction.x < 0 && this.isFacingRight && !this.isAttacking)
        {
            Flip();
            this.SecondAttack();
        }

        if(Mathf.Abs(distanceFromPlayer) < this.attackZone)
        {
            this.FirstAttack();
        }

        if (Vector2.Distance(transform.position, targetPoint.position) < 2f)
        {
            this.currentPatrolPoint = (this.currentPatrolPoint + 1) % patrolPoints.Length;
        }
    }

    public void FirstAttack()
    {
        this.animator.SetTrigger("Atk1");
    }

    public void SecondAttack()
    {
        this.animator.SetTrigger("Atk2");
    }

    public void ThunderAttack()
    {
        Vector2 posAttack = new Vector2(this.player.position.x, this.firePoint.position.y + 4.7f);
        Transform newBullet = BulletSpawner.Instance.Spawn("Bullet_4", posAttack, this.firePoint.rotation);
        newBullet.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        if (newBullet.GetComponent<DamageSender>() == null && newBullet.GetComponentInChildren<DamageSender>() == null) return;
        newBullet.gameObject.SetActive(true);
    }
}
