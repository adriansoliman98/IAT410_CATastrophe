using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;

    //planning to add health bar in the future
    //public EnemyHealthBar enemyhealthBar;

    public float moveSpeed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;

    public LayerMask playerMask;

    private Transform target;
    public Rigidbody2D rb;
    private Vector2 movement;
    public Vector3 dir;
    public Animator enemyAnimator;

    private bool isInChaseRange;
    private bool isInAttackRange;

    Vector2 moveDirection;


    private void Start()
    {
        health = maxHealth;

        target = GameObject.FindWithTag("Player").transform;
     //   enemyhealthBar.SetHealth(health, maxHealth);
    }

    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, playerMask);


        dir = target.position - transform.position;

        dir.Normalize();
        movement = dir;
        ProcessInputs();

    }

    private void FixedUpdate()
    {

        if (isInChaseRange)
        {
            DoAnimations();
            MoveCharacter();

        }

        else
        {

            rb.velocity = Vector2.zero;
        }


    }

    private void MoveCharacter()
    {
        rb.MovePosition((Vector2)transform.position + (movement * moveSpeed * Time.deltaTime));


    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        moveDirection = new Vector2(moveX, moveY).normalized;

    }

    void DoAnimations()
    {
        enemyAnimator.SetFloat("Horizontal", moveDirection.x);
        enemyAnimator.SetFloat("Vertical", moveDirection.y);
        enemyAnimator.SetFloat("Speed", moveDirection.magnitude);

        //animator.SetFloat("Speed", moveSpeed);
    }

    // Update is called once per frame
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;



        if (health <= 0)
        {
            Destroy(this.gameObject);
            // GetComponent<ItemSpawner>().Spawn();
            GetComponent<LootBag>().InstantiateLoot(transform.position);
           
        
        }


    }
}
