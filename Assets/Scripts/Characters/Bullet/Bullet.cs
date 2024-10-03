using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Bullet : MyMonobehaviour, IDFly
{
    [SerializeField] protected Rigidbody2D rb;
    [SerializeField] protected Vector3 direction;

    protected override void OnEnable()
    {
        this.SetDirection(direction);
        this.SetRotation(direction);
        this.Flip();
    }

    protected override void LoadComponents()
    {
        this.rb = GetComponent<Rigidbody2D>();
    }

    protected void Update()
    {
        this.Fly();
    }

    public void Fly()
    {
        this.rb.velocity = this.direction * 10f;
    }

    protected void Flip()
    {
        if(this.direction.x == -1)
        {
            Vector3 localSacle = transform.localScale;
            localSacle.y *= -1;
            transform.localScale = localSacle;
        }
    }

    public void SetDirection(Vector2 vector)
    {
        this.direction = vector;
    }

    public void SetRotation(Vector2 vector)
    {
        float angle = Mathf.Atan2(this.direction.y, this.direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
    