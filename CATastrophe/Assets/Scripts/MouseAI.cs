using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class MouseAI : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public ParticleScript particleScript;


    //planning to add health bar in the future
    public EnemyHealthBar enemyhealthBar;

    private BulletControl bulletControl;

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
    public Vector3 lastVelocity;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {

        
        health = maxHealth;
        enemyhealthBar.SetHealth(health, maxHealth);
        target = GameObject.FindWithTag("Player").transform;
        rb.velocity = new Vector2(15f, 15f);
    }

    private void Update()
    {
       // rb.AddForce(new Vector2(9.8f * 25f, 9.8f * 25f));
        // isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, playerMask);
        lastVelocity = rb.velocity;
        
        // dir = target.position - transform.position;

        //   dir.Normalize();
        //  movement = dir;


    }

    private void FixedUpdate()
    {

    
    }


    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Catgun")
        {
            // enemyComponent.TakeDamage(bulletDamage);
            // Destroy(gameObject);

        }
        else
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector3.Reflect(lastVelocity.normalized, coll.contacts[0].normal);

            rb.velocity = direction * Mathf.Max(speed, 12f);
        }
    }


    private void MoveCharacter()
    {
      //  rb.MovePosition((Vector2)transform.position + (movement * moveSpeed * Time.deltaTime));
      lastVelocity = rb.velocity;

    }

    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

      //  moveDirection = new Vector2(moveX, moveY).normalized;

    }

    void DoAnimations()
    {
        enemyAnimator.SetFloat("enemyHorizontal", moveDirection.x);
        enemyAnimator.SetFloat("enemyVertical", moveDirection.y);
        enemyAnimator.SetFloat("enemySpeed", moveDirection.magnitude);

        //animator.SetFloat("Speed", moveSpeed);
    }

    // Update is called once per frame
    public void TakeDamage(float damageAmount)
    {
        health -= damageAmount;

        enemyhealthBar.SetHealth(health, maxHealth);

        if (health <= 0)
        {
            Destroy(this.gameObject);
            // GetComponent<ItemSpawner>().Spawn();
         //   GetComponent<LootBag>().InstantiateLoot(transform.position);
          //  Instantiate(particleScript, transform.position, Quaternion.identity);

        }


    }




}
