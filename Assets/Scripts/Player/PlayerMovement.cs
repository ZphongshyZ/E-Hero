using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MyMonobehaviour
{
    [SerializeField] protected float speed;
    [SerializeField] protected float jumpPower;
    [SerializeField] protected float horizontal;

    [SerializeField] protected float rollDuration;
    [SerializeField] protected float rollPower;
    [SerializeField] protected bool isRolling = false;

    [SerializeField] protected bool isFacingRight = true;

    [SerializeField] protected Vector2 direction = Vector2.right;

    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected CapsuleCollider2D body;
    [SerializeField] protected Animator animator;
    [SerializeField] protected GameObject groundCheck;
    [SerializeField] protected Character character;
    [SerializeField] protected DamageReceiver damageReceiver;
    [SerializeField] protected LayerMask groundLayer;

    protected override void LoadComponents()
    {
        if (this.rb != null && this.animator != null && this.character != null && this.groundCheck != null) return;
        this.rb = GetComponent<Rigidbody2D>();
        this.body = GetComponent<CapsuleCollider2D>();
        this.animator = GetComponent<Animator>();
        this.character = GetComponent<Character>();
        this.damageReceiver = GetComponent<DamageReceiver>();
        this.groundCheck = GameObject.Find("Foot");
        this.SetProperties();
    }

    protected void SetProperties()
    {
        this.speed = this.character.characterSO.speed;
        this.jumpPower = this.character.characterSO.jumpPower;
        this.rollPower = this.character.characterSO.rollPower;
    }

    protected void Update()
    {
        this.SetDirection();
        this.character.setDirection(this.direction);
        this.Move();
        this.Flip();
        this.SetAnimation();
    }

    //Animation
    protected void SetAnimation()
    {
        this.animator.SetFloat("Vertical", this.rb.velocity.y);
        this.animator.SetBool("isGrounded", IsGrounded());
        if (this.horizontal != 0)
        {
            this.animator.SetBool("isRunning", true);
        }
        else
        {
            this.animator.SetBool("isRunning", false);
        }
    }

    //Move
    protected void OnMovement(InputValue value)
    {
        this.horizontal = value.Get<float>();
    }

    protected void Move()
    {
        if (this.isRolling) return;
        if (this.character.IsAttacking || this.damageReceiver._IsDead)
        {
            this.rb.velocity = Vector2.zero;
            return;
        }
        this.rb.velocity = new Vector2(this.horizontal * this.speed, this.rb.velocity.y);
    }

    protected void Flip()
    {
        if (this.damageReceiver._IsDead) return;
        if (this.character.IsAttacking) return;
        if (this.horizontal < 0 && this.isFacingRight || this.horizontal > 0 && !this.isFacingRight)
        {
            this.isFacingRight = !this.isFacingRight;
            Vector3 localSacle = this.transform.localScale;
            localSacle.x *= -1;
            this.direction.x *= -1;
            this.transform.localScale = localSacle;
        }
    }

    //Jump
    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.transform.position, 0.25f, groundLayer);
    }

    protected void OnJump()
    {
        if (this.damageReceiver._IsDead) return;
        if (IsGrounded() && !this.character.IsAttacking && !this.isRolling)
        {
            this.direction = new Vector2(this.direction.x, -1); 
            this.rb.velocity = new Vector2(this.rb.velocity.x, this.jumpPower);
        }
    }

    protected void SetDirection()
    {
        if(!IsGrounded())
        {
            this.direction.y = -1;
        }
        else
        {
            this.direction.y = 0;
        }
    }

    //Roll
    protected void OnRoll()
    {
        if (this.damageReceiver._IsDead) return;
        if (this.IsGrounded() && !this.character.IsActiving && this.horizontal != 0 && !this.character.IsAttacking)
        {
            StartCoroutine(Roll());
        }
    }

    protected IEnumerator Roll()
    {
        this.isRolling = true;
        float startTime = Time.time;
        this.damageReceiver.IsImortal = true;
        int enemyLayerIndex = LayerMask.NameToLayer("Enermy");
        this.rb.velocity = new Vector2(transform.localScale.x * this.rollPower, rb.velocity.y);
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayerIndex, true);
        this.animator.SetTrigger("Roll");
        while (Time.time < startTime + rollDuration)
        {
            yield return null;
        }
        this.damageReceiver.IsImortal = false;
        this.isRolling = false;
        Physics2D.IgnoreLayerCollision(gameObject.layer, enemyLayerIndex, false);
    }
}
