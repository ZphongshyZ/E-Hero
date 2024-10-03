using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeFly : Monster, IDFirstAttack
{
    [SerializeField] protected Transform firePoint;

    protected override void Update()
    {
        this.LookAtTarget();
        base.Update();
    }

    protected override void Patrol()
    {
        if (this.isDead)
        {
            this.rb.velocity = Vector2.down * 5f;
            return;
        }
        if (this.isAttacking)
        {
            this.rb.velocity = Vector2.zero;
            return;
        }
        float distanceFromPlayer = this.transform.position.x - this.player.position.x;
        this.time += Time.deltaTime;
        if (Mathf.Abs(distanceFromPlayer) > this.patrolZone / 2 || this.player.position.x > this.patrolPoints[1].position.x)
        {

            if (patrolPoints.Length == 0) return;
            this.animator.SetBool("isRunning", true);
            Transform targetPoint = patrolPoints[this.currentPatrolPoint];
            Vector2 direction = (targetPoint.position - transform.position).normalized;
            this.rb.velocity = new Vector2(direction.x * this.speed, 0);

            if (direction.x > 0 && !this.isFacingRight && !this.isAttacking)
            {
                Flip();
            }
            else if (direction.x < 0 && this.isFacingRight && !this.isAttacking)
            {
                Flip();
            }

            if (Vector2.Distance(transform.position, targetPoint.position) < 1f)
            {
                this.currentPatrolPoint = (this.currentPatrolPoint + 1) % patrolPoints.Length;
            }
        }
        else if (Mathf.Abs(distanceFromPlayer) < this.patrolZone / 2 && this.player.position.x <= this.patrolPoints[1].position.x)
        {
            Vector2 direction = (this.player.position - this.transform.position).normalized;
            this.rb.velocity = new Vector2(direction.x * this.speed, 0);
            if (direction.x > 0 && !this.isFacingRight)
            {
                Flip();
            }
            else if (direction.x < 0 && this.isFacingRight)
            {
                Flip();
            }
        }
        if (Mathf.Abs(distanceFromPlayer) < this.attackZone / 2)
        {
            this.Attack();
        }
    }

    private void LookAtTarget()
    {
        Vector3 difference = this.player.position - this.firePoint.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        this.firePoint.rotation = Quaternion.Euler(0f, 0f, rotationZ);
    }

    public void Shoot()
    {
        Transform newBullet = BulletSpawner.Instance.Spawn("Bullet_3", this.firePoint.position, this.firePoint.rotation);
        if (newBullet.GetComponent<DamageSender>() == null) return;
        if (newBullet.GetComponent<Bullet>() == null) return;
        newBullet.gameObject.SetActive(true);
        Vector2 direction = (this.player.position - firePoint.position).normalized;
        newBullet.GetComponent<Bullet>().SetDirection(direction);
        this.time = 0f;
    }

    public void FirstAttack()
    {
        this.animator.SetTrigger("Atk1");
    }

    protected override void Attack()
    {
        if (this.time < this.timeAttack) return;
        this.FirstAttack();
    }
}
