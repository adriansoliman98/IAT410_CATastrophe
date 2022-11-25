using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using static WeaponItem;

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

    public ParticleScript particleScript;

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

    public Rigidbody2D rb;
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
    
    private Vector3  newBullet2,newBullet3, bulletDir, topBullet, bottomBullet, bottomBulletVel;
    private Quaternion newBulletRotation;
    private List<WeaponItem> weaponList;
    private WeaponItem weaponItem;
    public WeaponType weaponType;
    public int amount;
    //called once at startup
    private void Awake()
    {
     //   WeaponWorld.SpawnWeaponWorld(new Vector3(2,2), new WeaponItem { weaponType = WeaponItem.WeaponType.MinecraftBow, amount = 1 });
      
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        WeaponWorld weaponWorld = collider.GetComponent<WeaponWorld>();
        if (weaponWorld != null)
        {
            inventory.AddWeapon(weaponWorld.GetWeapon());
            weaponWorld.DestorySelf();
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
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

                fireDelay = 0.1f;
            if (weaponSwitch.WeaponCash == true)
            {
                Shoot(shootHor, shootVert);

            }
            if (weaponSwitch.WeaponArrow == true)
            {
                Shoot3(shootHor, shootVert);

            }
            if (weaponSwitch.WeaponMelee == true)
            {
                Shoot2(shootHor, shootVert);

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
        


       
        bulletControl = player.GetComponent<BulletControl>();

        if (uiInventory.knifeAmount == 1)
        {
            GameObject bulletMelee = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bulletMelee.GetComponent<Rigidbody2D>().velocity = new Vector3(



        (x < 0) ? Mathf.Floor(x) * bulletSpeed2 : Mathf.Ceil(x) * bulletSpeed2,
        (y < 0) ? Mathf.Floor(y) * bulletSpeed2 : Mathf.Ceil(y) * bulletSpeed2,
        0
    );

        }

        if (uiInventory.knifeAmount == 2)
        {
         //   bulletPrefab.transform.localScale = new Vector3(13, 13, 0);
            GameObject bulletMelee2 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bulletMelee2.GetComponent<Rigidbody2D>().velocity = new Vector3(



        (x < 0) ? Mathf.Floor(x) * 3 : Mathf.Ceil(x) * 3,
        (y < 0) ? Mathf.Floor(y) * 3 : Mathf.Ceil(y) * 3,
        0
    );

        }
    }
    

    private void WeaponUpgrade(WeaponItem weaponItem, float x, float y)
    {
        

        foreach (WeaponItem weapon in inventory.GetWeaponList())
        {
            if (weapon.weaponType == WeaponType.SupremeGun)
            {
                if (weaponItem.amount > 0)
                {
                    bulletControl = player.GetComponent<BulletControl>();
                    GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                    
                    bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(



                   (x < 0) ? Mathf.Floor(x) * 1 * bulletSpeed + moveDirection.x * 13 : Mathf.Ceil(x) * bulletSpeed + moveDirection.x * 13,
                   (y < 0) ? Mathf.Floor(y) * 1 * bulletSpeed + moveDirection.y * 13 : Mathf.Ceil(y) * bulletSpeed + moveDirection.y * 13,
                   0
               );
                }
            }

        }
        
    }

    public void Shoot(float x, float y)
    {


     //   UIinventory = UIinventory.GetComponent<UI_Inventory>();
        userAnimator.SetTrigger("MoneyAttack");
        userAnimator.SetFloat("BulletHorizontal", x);
        userAnimator.SetFloat("BulletVertical", y);

        topBullet = new Vector3(transform.position.x + 1, transform.position.y +1  );
        bottomBullet = new Vector3(transform.position.x - 1, transform.position.y - 1);
       
        newBulletRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y,  transform.rotation.z+50);
        newBullet2 = Quaternion.Euler(0,0, 30f) * bulletDir;
        newBullet3 = Quaternion.Euler(0, 0, -30f) * bulletDir;
         bottomBulletVel = Quaternion.Euler(0, 0, 0) * bulletDir;


        bulletControl = player.GetComponent<BulletControl>();

        bulletDir = new Vector3(
        (x < 0) ? Mathf.Floor(x) * 1 * bulletSpeed + moveDirection.x * 13 : Mathf.Ceil(x) * bulletSpeed + moveDirection.x * 13,
        (y < 0) ? Mathf.Floor(y) * 1 * bulletSpeed + moveDirection.y * 13 : Mathf.Ceil(y) * bulletSpeed + moveDirection.y * 13,
        0);



        if (uiInventory.supremeGunAmount == 1)
        {
            
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            //  bulletControl = player.GetComponent<BulletControl>();
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDir;



        }

        if (uiInventory.supremeGunAmount == 2)
        {

            GameObject top= Instantiate(bulletPrefab, bottomBullet, transform.rotation) as GameObject;
            //  bulletControl = player.GetComponent<BulletControl>();
            top.GetComponent<Rigidbody2D>().velocity = bulletDir;

            GameObject bottom = Instantiate(bulletPrefab, topBullet, transform.rotation) as GameObject;
            //  bulletControl = player.GetComponent<BulletControl>();
            bottom.GetComponent<Rigidbody2D>().velocity = bulletDir;



        }

        if (uiInventory.supremeGunAmount == 3)
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
          //  bulletControl = player.GetComponent<BulletControl>();
            bullet.GetComponent<Rigidbody2D>().velocity = bulletDir;
                       

          GameObject bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet2.GetComponent<Rigidbody2D>().velocity = newBullet2;         

   
            GameObject bullet3 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet3.GetComponent<Rigidbody2D>().velocity = newBullet3;
          
        }


        if (uiInventory.supremeGunAmount == 4)
        {
            GameObject top = Instantiate(bulletPrefab, bottomBullet, transform.rotation) as GameObject;
            //  bulletControl = player.GetComponent<BulletControl>();
            top.GetComponent<Rigidbody2D>().velocity = bulletDir;

            GameObject bottom = Instantiate(bulletPrefab, topBullet, transform.rotation) as GameObject;
            //  bulletControl = player.GetComponent<BulletControl>();
            bottom.GetComponent<Rigidbody2D>().velocity = bulletDir;


            GameObject bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet2.GetComponent<Rigidbody2D>().velocity = newBullet2;


            GameObject bullet3 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bullet3.GetComponent<Rigidbody2D>().velocity = newBullet3;

        }
    }

        
        //WeaponUpgrade(weaponItem,x,y);

        /*  foreach (WeaponItem inventoryWeapon in weaponList)
          {
              if (WeaponType.SupremeGun.amount == 2)
              {
                  GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                  GameObject bullet2 = Instantiate(bulletPrefab, newBullet, transform.rotation) as GameObject;

                  bulletControl = player.GetComponent<BulletControl>();
                  bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(



                  (x < 0) ? Mathf.Floor(x) * 1 * bulletSpeed + moveDirection.x * 13 : Mathf.Ceil(x) * bulletSpeed + moveDirection.x * 13,
                  (y < 0) ? Mathf.Floor(y) * 1 * bulletSpeed + moveDirection.y * 13 : Mathf.Ceil(y) * bulletSpeed + moveDirection.y * 13,
                  0
              );

                  bullet2.GetComponent<Rigidbody2D>().velocity = new Vector3(



                   (x < 0) ? Mathf.Floor(x) * 1 * bulletSpeed + moveDirection.x * 13 : Mathf.Ceil(x) * bulletSpeed + moveDirection.x * 13,
                   (y < 0) ? Mathf.Floor(y) * 1 * bulletSpeed + moveDirection.y * 13 : Mathf.Ceil(y) * bulletSpeed + moveDirection.y * 13,
                   0
               );

              }
              else if(weaponItem.amount == 1)
              {

          */
      
            
        

        

    

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

            Instantiate(particleScript, transform.position, Quaternion.identity);
            //enemyAI.PlayerHit();
            if (playerHealth < 1)
            {
                Destroy(gameObject);
            }
            
                
        }

        if (collision.gameObject.tag == "firstBoss")
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            //Remove if we want player health
            Vector2 difference = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);

            playerHealth--;
            print(playerHealth);

            Instantiate(particleScript, transform.position, Quaternion.identity);
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
            Vector2 difference = transform.position - collision.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);

            playerHealth--;
            Instantiate(particleScript, transform.position, Quaternion.identity);
            //print(playerHealth);
            //enemyAI.PlayerHit();
            if (playerHealth < 1)
            {
                Destroy(gameObject);
            }


        }
    }
}
