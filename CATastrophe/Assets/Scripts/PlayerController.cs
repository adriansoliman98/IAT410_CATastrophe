using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
 

    int totalWeapons = 1;
    public int currentWeaponIndex;

    public float playerHealth = 3;
   public float playerMaxHealth = 3f;

    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;
    public string A = "Arrow";



    BulletControl bulletControl;
    WeaponSwitch weaponSwitch;
    EnemyAI enemyAI;
    public GameObject player;
    public GameObject player2;
    public GameObject bulletPrefab;
    private Vector2 moveDirection;

    public float moveSpeed;


    public bool catgun = true;
    public bool bow = false;

    GameObject Bullet;

    new Rigidbody2D rb;
    // public Text collectedText;
    public static int collectedAmount = 0;

    //  public GameObject bulletPrefab;

    public float bulletSpeed;
    public float bulletSpeed2;
    public float lastFire;
    private float fireDelay;
    public int Respawn;

    public Animator userAnimator;

    //inventory
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    //called once at startup
    private void Awake()
    {
        
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
    }


    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();

        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);

        }

        bulletControl = player.GetComponent<BulletControl>();

        weaponSwitch = player2.GetComponent<WeaponSwitch>();

        enemyAI = player2.GetComponent<EnemyAI>();  

    }

    // Update is called once per frame
    void Update()
    {

        bulletControl = player.GetComponent<BulletControl>();
        weaponSwitch = player2.GetComponent<WeaponSwitch>();
        //  bulletControl.bulletSpeed = bulletSpeed;
        SetBulletPreFab(bulletPrefab);
        //   fireDelay = GameController.FireRate;
        //   speed = GameController.MoveSpeed;


        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");
        //   print(bulletSpeed);


        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)
        {

            if (weaponSwitch.WeaponCash == true)
            {
                fireDelay = 0.1f;
                Shoot(shootHor, shootVert);

            }

            else
            if (weaponSwitch.WeaponMelee == true)
            {
                fireDelay = 0.8f;
                Shoot2(shootHor, shootVert);

            }

            else
            if (weaponSwitch.WeaponArrow == true)
            {
                fireDelay = 0.5f;
                Shoot3(shootHor, shootVert);

            }
            lastFire = Time.time;
        }


       
        



    }
    /// <summary>
    /// ANIMATIONS
    /// </summary>
    void ProcessInputs()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");


        moveDirection = new Vector2(moveX, moveY).normalized;


    }

    void DoAnimations()
    {
        //checkKey();
        //checkKeyMove();
        userAnimator.SetFloat("Horizontal", moveDirection.x);
        userAnimator.SetFloat("Vertical", moveDirection.y);
        userAnimator.SetFloat("Speed", moveDirection.sqrMagnitude);

    }

    void checkKey()
    {
        if (Input.GetKeyDown("up"))
        {
            userAnimator.SetTrigger("MeleeAttack");
            userAnimator.SetFloat("BulletVertical", 1);
            userAnimator.SetFloat("BulletHorizontal", 0);
        }

        else if (Input.GetKeyDown("down"))
        {
            userAnimator.SetTrigger("MeleeAttack");
            userAnimator.SetFloat("BulletVertical", -1);
            userAnimator.SetFloat("BulletHorizontal", 0);
        }

        else if (Input.GetKeyDown("left"))
        {
            userAnimator.SetTrigger("MeleeAttack");
            userAnimator.SetFloat("BulletVertical", 0);
            userAnimator.SetFloat("BulletHorizontal", -1);
        }

        else if (Input.GetKeyDown("right"))
        {
            userAnimator.SetTrigger("MeleeAttack");
            userAnimator.SetFloat("BulletVertical", 0);
            userAnimator.SetFloat("BulletHorizontal", 1);
        }


    }

    void checkKeyMove()
    {
        if (Input.GetKeyDown("w"))
        {
            userAnimator.SetFloat("Horizontal", 0);
            userAnimator.SetFloat("Vertical", 1);
        }

        else if (Input.GetKeyDown("a"))
        {
            userAnimator.SetFloat("Horizontal", -1);
            userAnimator.SetFloat("Vertical", 0);
        }

        else if (Input.GetKeyDown("s"))
        {
            userAnimator.SetFloat("Horizontal", 0);
            userAnimator.SetFloat("Vertical", -1);
        }

        else if (Input.GetKeyDown("d"))
        {
            userAnimator.SetFloat("Horizontal", 1);
            userAnimator.SetFloat("Vertical", 0);
        }
    }

    private void FixedUpdate()
    {
        Move();
        ProcessInputs();
        DoAnimations();

    }

    public void SetBulletPreFab(GameObject newBullet)
    {
        bulletPrefab = newBullet;

    }

    void Move()
    {

        rb.velocity = new Vector2(moveDirection.x * moveSpeed, moveDirection.y * moveSpeed);
    }



    public void GainWeapon()
    {
        Destroy(gameObject);
    }

   

   public void Shoot2(float x, float y)
    {
        userAnimator.SetTrigger("MeleeAttack");
        userAnimator.SetFloat("BulletHorizontal", x);
        userAnimator.SetFloat("BulletVertical", y);
        


        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;

        bulletControl = player.GetComponent<BulletControl>();
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(



        (x < 0) ? Mathf.Floor(x) * bulletSpeed2 : Mathf.Ceil(x) * bulletSpeed2,
        (y < 0) ? Mathf.Floor(y) * bulletSpeed2 : Mathf.Ceil(y) * bulletSpeed2,
        0
    );
    }

    public void Shoot(float x, float y)
    {



        userAnimator.SetTrigger("MoneyAttack");
        userAnimator.SetFloat("BulletHorizontal", x);
        userAnimator.SetFloat("BulletVertical", y);
        

        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;

        bulletControl = player.GetComponent<BulletControl>();
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(



        (x < 0) ? Mathf.Floor(x) * bulletSpeed + moveDirection.x : Mathf.Ceil(x) * bulletSpeed + moveDirection.x,
        (y < 0) ? Mathf.Floor(y) * bulletSpeed + moveDirection.y : Mathf.Ceil(y) * bulletSpeed+ moveDirection.y,
        0
    );



    }

    public void Shoot3(float x, float y)
    {
        userAnimator.SetTrigger("BottleAttack");
        userAnimator.SetFloat("BulletHorizontal", x);
        userAnimator.SetFloat("BulletVertical", y);
        


        GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;

        bulletControl = player.GetComponent<BulletControl>();
        bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(



        (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
        (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
        0
    );
    }

    /*private void OnCollisionEnter2D(Collider other)
    {
        if (other.tag == "Enemy")
        {

            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }
    */

    private void OnCollisionEnter2D(Collision2D collision)
    {

        

        if (collision.gameObject.tag == "Enemy")
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            //Remove if we want player health
            Vector2 difference = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);

            playerHealth--;
            print(playerHealth);
            //enemyAI.PlayerHit();
            if (playerHealth < 1)
            {
                Destroy(gameObject);
            }
            
                
        }


        if (collision.gameObject.tag == "BossBullet")
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            //Remove if we want player health


            playerHealth = playerHealth - 3;
            print(playerHealth);
            //enemyAI.PlayerHit();
            if (playerHealth < 1)
            {
                Destroy(gameObject);
            }


        }
    }
}
