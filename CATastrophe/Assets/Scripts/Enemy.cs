using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public float health;
    public float maxHealth;

    public GameObject particleScript;
    public bool bossDead;

    //planning to add health bar in the future
    public EnemyHealthBar enemyhealthBar;

    public float moveSpeed;
    public float checkRadius;
    public float attackRadius;

    public bool shouldRotate;
    public bool dropItem;

    public LayerMask playerMask;

    private Transform target;
    public Rigidbody2D rb;
    private Vector2 movement;
    public Vector3 dir;
    public Animator enemyAnimator;

    private bool isInChaseRange;
    private bool isInAttackRange;

    Vector2 moveDirection;

    Vector3 startPosition;


    private void Awake()
    {
        //Vector3 currentPosition = new Vector3(transform.position.x, transform.position.y, 0);
        //this.originalPosition = this.transform.position;

        startPosition = new Vector3(-1434.1f, 963f, 0);
    }

    private void Start()
    {
        health = maxHealth;
        enemyhealthBar.SetHealth(health, maxHealth);
        target = GameObject.FindWithTag("Player").transform;
        if (health == -1)
        {
            Destroy(this.gameObject);
            // GetComponent<ItemSpawner>().Spawn();
            Instantiate(particleScript, transform.position, Quaternion.identity);
            if (dropItem == true)
            {

                GetComponent<WeaponWorldSpawner>().InstantiateLoot();
            }
        }
    }

    private void Update()
    {
        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, playerMask);


        dir = target.position - transform.position;

        dir.Normalize();
        movement = dir;

      //  print(moveDirection.x);
    }

    private void FixedUpdate()
    {

        if (isInChaseRange)
        {

            MoveCharacter();
            ProcessInputs();
            DoAnimations();
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
       // float moveX2 = Input.GetAxisRaw("Horizontal");
       // float moveY2 = Input.GetAxisRaw("Vertical");

       // moveDirection = new Vector2(moveX2, moveY2).normalized;
        moveDirection = movement;
    }

    void DoAnimations()
    {
        enemyAnimator.SetFloat("enemyHorizontal", moveDirection.x);
        enemyAnimator.SetFloat("enemyVertical", moveDirection.y);
        enemyAnimator.SetFloat("enemySpeed", moveDirection.magnitude);

        
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
            Instantiate(particleScript, transform.position, Quaternion.identity);
            if (dropItem == true)
            {

                GetComponent<WeaponWorldSpawner>().InstantiateLoot();

            }
            //if (gameObject.tag == "firstBoss")
            //  {


        }

        if (this.gameObject.tag == "FirstBoss")
        {
            if (health <= 0)
            {
                //   bossDead = true;
            }
        }

        if (this.gameObject.tag == "thirdBoss")
        {
            if (health <= 0)
            {
                   bossDead = true;
            }

        }
    }

    public void ResetBossPosition()
    {
        if (gameObject.tag == "thirdBoss")
         {
            // transform.position = startPosition;

            rb.MovePosition(startPosition);
          }
        print("hello");
    }



}
