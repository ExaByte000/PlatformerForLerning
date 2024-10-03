using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;

public class Hero : Entity
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 13f;

    public Transform attackPos;
    public float attackRange;
    public LayerMask enemy;

    private bool isGrounded = true;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    public bool isAttacking = false;
    public bool isRecharged = true;
    //private bool comboCheck = false;
   // private bool comboAttack = false;
    //private bool firsAttackFinihs = false;

    public static Hero Instance { get; set; }

    private States State
    {
        get { return (States)anim.GetInteger("State"); }
        set { anim.SetInteger("State", (int)value); }
    }

    private void Awake()
    {
        Instance = this;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        isRecharged = true;
    }
    private void Start()
    {
        lives = 5;
    }

    private void FixedUpdate()
    {
        CheckGroug();
    }

    private void Update()
    {
        if (isGrounded) State = States.Idle;
        if (isAttacking) State = States.Attack;  
        

        if (Input.GetButton("Horizontal")) Run();
        if (isGrounded && Input.GetButtonDown("Jump")) Jump();
        if (Input.GetButtonDown("Fire1")) Attack();
      
    }

    private void Jump()
    {

        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void Run()
    {
        if (isGrounded) State = States.Run;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        sprite.flipX = dir.x < 0f;
        if (dir.x < 0f) attackPos.localPosition = new(-1.22f, attackPos.localPosition.y);
        else attackPos.localPosition = new(1.22f, attackPos.localPosition.y);
        
    }

     private void CheckGroug()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
        if (transform.position.y < -30f)
        {
            transform.position = new(-6.88f, -3.24f);
            lives--;
        }
        if (!isGrounded && rb.velocity.y > 0f) State = States.Jump;
        else if (!isGrounded && rb.velocity.y < 0f) State = States.Fall;
    }


    private void Attack()
    {
        if (isRecharged)
        {
            isAttacking = true;
            isRecharged = false;
            StartCoroutine(AttackAnimation());
            StartCoroutine(AttackCoolDown());
        }
        
    }
    

    public void OnAttack()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(attackPos.position, attackRange, enemy);

        for (int i = 0; i < colliders.Length; i++)
        {
            colliders[i].GetComponent<Entity>().GetDamage();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private IEnumerator AttackAnimation()
    {
        yield return new WaitForSeconds(1f);

        isAttacking = false;
    }
    private IEnumerator AttackCoolDown()
    {
        yield return new WaitForSeconds(1.1f);
        isRecharged = true;
    }
}

public enum States
{
    Idle,
    Run,
    Jump,
    Fall,
    Attack,
    ComboAttack
}
