using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Character
{
    //Patrol
    [SerializeField] protected int currentPatrolPoint = 0;
    [SerializeField] protected float speed = 2f;
    [SerializeField] protected float patrolZone = 20f;
    [SerializeField] protected float attackZone = 10f;

    //Attack
    [SerializeField] protected float time = 0f;
    [SerializeField] protected float timeAttack;

    [SerializeField] protected bool isFacingRight = true;
    [SerializeField] protected bool isDead = false;

    [SerializeField] protected Transform[] patrolPoints;
    [SerializeField] protected Transform player;
    [SerializeField] protected Rigidbody2D rb;
    protected override void LoadComponents()
    {
        this.rb = GetComponent<Rigidbody2D>();
        this.player = GameObject.FindGameObjectWithTag("Player").transform;
        base.LoadComponents();
    }

    protected virtual void Update()
    {
        this.Patrol();
    }

    //Patrol
    protected virtual void Patrol()
    {
        if (this.isDead || isAttacking)
        {
            this.rb.velocity = Vector2.zero;
            return;
        }
        float distanceFromPlayer = this.transform.position.x - this.player.position.x;
        if(Mathf.Abs(distanceFromPlayer) > this.patrolZone/2 || this.player.position.x > this.patrolPoints[1].position.x)
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
        else if(Mathf.Abs(distanceFromPlayer) < this.patrolZone / 2 && this.player.position.x <= this.patrolPoints[1].position.x)
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
        if(Mathf.Abs(distanceFromPlayer) < this.attackZone / 2)
        {
            this.Attack();
        }
    }

    protected void Flip()
    {
        this.isFacingRight = !this.isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1;
        transform.localScale = localScale;
    }

    protected void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(this.transform.position, new Vector2(patrolZone, 5f));
        Gizmos.DrawWireCube(this.transform.position, new Vector2(attackZone, 5f));
    }

    //Dead
    public void Dead()
    {
        this.isDead = true;
    }

    protected virtual void Attack()
    {

    }
}
