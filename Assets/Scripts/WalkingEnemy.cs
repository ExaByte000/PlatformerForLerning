using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingEnemy : Entity
{
    private float speed = 3.5f;
    private Vector3 dir;
    private SpriteRenderer sprite;

    private void Awake()
    {
        sprite = GetComponentInChildren<SpriteRenderer>();
    }

    private void Start()
    {
        dir = transform.right;  
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position + transform.up * 0.2f + transform.right * dir.x * 0.7f, 0.1f);

        if (collider.Length > 0)
        {
            dir *= -1f;
            sprite.flipX = !sprite.flipX;
        }
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Hero.Instance.gameObject)
        {
            Hero.Instance.GetDamage();
        }
    }
}
