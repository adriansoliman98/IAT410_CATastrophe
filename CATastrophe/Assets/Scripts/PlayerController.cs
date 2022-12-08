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

    public float playerHealth = 2;
    public float playerMaxHealth = 3f;

    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;
    public string A = "Arrow";

    public ParticleScript particleScript;

    BulletControl bulletControl;
    WeaponSwitch weaponSwitch;
    EnemyAI enemyAI;
    public GameObject enemy;
    public Teleport teleport;
    public GameObject player;
    public GameObject player2;
    public GameObject bulletPrefab;
    private Vector2 moveDirection;
    private Vector2 moveFire;

    public float moveSpeed;
    int fireLimit = 1;

    public bool catgun = true;
    public bool bow = false;

    GameObject Bullet;

    public Rigidbody2D rb;
    // public Text collectedText;
    public static int collectedAmount = 0;

    //  public GameObject bulletPrefab;

    public float bulletSpeed, bulletSpeed4;
    public float bulletSpeed2, bulletSpeed3;
    public float airSpeed;
    public float lastFire;
    private float fireDelay;
    private float SprayDelay;
    public int Respawn;
    public bool inlevel2, inlevel3, finallevel;

    public Animator userAnimator;

    //inventory
    [SerializeField] private UI_Inventory uiInventory;
    private Inventory inventory;

    private Vector2 level2start, level3start, levelRespawn;

    private Vector3 flameBulletRight, flameBulletLeft, flameBulletUp, flameBulletDown;
    private Vector3 newBullet2, newBullet3, bulletDir, knifeDir, sprayDir, sprayDir2, topBullet, bottomBullet, bottomBulletVel;
    private Vector3 topSpray, bottomSpray, fireDir;
    private Vector3 newKnife, newKnife2, newKnife3;
    private Vector3 newSpray, newSpray2, newSpray3;
    private Quaternion newBulletRotation, sideBullet, upSprite, leftSprite, downSprite;
    private List<WeaponItem> weaponList;
    private WeaponItem weaponItem;
    public WeaponType weaponType;
    public int amount;
     int startFire = 0;


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
        //enemy = player.GetComponent<Enemy>();
        teleport = player.GetComponent<Teleport>();
    }

 
    void Update()
    {
    
        teleport = player.GetComponent<Teleport>();
        bulletControl = player.GetComponent<BulletControl>();
        weaponSwitch = player2.GetComponent<WeaponSwitch>();
    
        SetBulletPreFab(bulletPrefab);
        

        float shootHor = Input.GetAxis("ShootHorizontal");
        float shootVert = Input.GetAxis("ShootVertical");
       

        if ((shootHor != 0 || shootVert != 0) && Time.time > lastFire + fireDelay)
        {

           // fireDelay = 0.1f;
            if (weaponSwitch.WeaponCash == true)
            {
                userAnimator.SetTrigger("MoneyAttack");
                fireDelay = 0.1f;
                Shoot(shootHor, shootVert);

            }
            if (weaponSwitch.WeaponAir == true)
            {
                userAnimator.SetTrigger("AirAttack");
                fireDelay = SprayDelay;
                ShootAir(shootHor, shootVert);

            }
            if (weaponSwitch.WeaponEarth == true)
            {
                userAnimator.SetTrigger("EarthAttack");
                //   lastFire = 0.5f;
                fireDelay = 0.2f;
                ShootEarth(shootHor, shootVert);

            }

            if (weaponSwitch.WeaponFire == true)
            {
                userAnimator.SetTrigger("FireAttack");
                //startFire = 1;
                fireDelay = 0.1f;
                ShootFlame(shootHor, shootVert);

            }



            if (weaponSwitch.WeaponWater == true)
            {
                userAnimator.SetTrigger("WaterAttack");
                //   lastFire = 0.5f;
                fireDelay = 0.1f;
                ShootWater(shootHor, shootVert);

            }

            lastFire = Time.time;
        }

       

        if (uiInventory.heartAmount > 0)
        {
            playerHealth += 1;
            uiInventory.heartAmount = 0;
        }


       // DontDestroyOnLoad(gameObject);
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

    public void Shootfire(float x, float y)
    {
       /* userAnimator.SetTrigger("MeleeAttack");
        userAnimator.SetFloat("BulletHorizontal", x);
        userAnimator.SetFloat("BulletVertical", y);

        moveFire = new Vector2(transform.position.x, transform.position.y);

        fireDir = new Vector3(
        (x < 0) ? Mathf.Floor(x) * 1 * 1 + moveDirection.x * 1 : Mathf.Ceil(x) * 1 + moveDirection.x * 1,
        (y < 0) ? Mathf.Floor(y) * 1 * 1 + moveDirection.y * 1 : Mathf.Ceil(y) * 1 + moveDirection.y * 1,
        0);

        //  fireDir = new Vector3(
        // (x < 0) ? Mathf.Floor(x) * 1 * 1 + transform.position.x * 1: Mathf.Ceil(x) * 1 + transform.position.x * 1,
        // (y < 0) ? Mathf.Floor(y) * 1 * 1 + moveDirection.y * 1 : Mathf.Ceil(y) * 1 + moveDirection.y * 1,
        // 0);

        bulletControl = player.GetComponent<BulletControl>();
        //if ((Input.GetKeyUp("up") && (Input.GetKeyUp("down")) && (Input.GetKeyUp("right")) && (Input.GetKeyUp("left"))))

        if((Input.GetKey(KeyCode.UpArrow) && (Input.GetKey(KeyCode.DownArrow) &&
            (Input.GetKey(KeyCode.RightArrow) && (Input.GetKey(KeyCode.LeftArrow) == false)))))
        {
            bulletControl.StopFire();
            startFire = 0;
        }
        if (startFire == 1) {

            if ((Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.DownArrow) ||
            (Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.LeftArrow) == false)))))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;

                startFire = 0;
            }
        }
       */
    }

    public void ShootFlame(float x, float y)
    {
        userAnimator.SetTrigger("MeleeAttack");
        userAnimator.SetFloat("BulletHorizontal", x);
        userAnimator.SetFloat("BulletVertical", y);

    
        flameBulletRight = new Vector3(transform.position.x + 5, transform.position.y);
        flameBulletLeft = new Vector3(transform.position.x- 5, transform.position.y);
        flameBulletUp = new Vector3(transform.position.x , transform.position.y + 5);
        flameBulletDown = new Vector3(transform.position.x , transform.position.y - 5);

        upSprite = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90);
        downSprite = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 270);
        leftSprite = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 180);

        fireDir = new Vector3(
        (x < 0) ? Mathf.Floor(x) * 1 * 1.5f + moveDirection.x * 18 : Mathf.Ceil(x) * 1.5f + moveDirection.x * 18,
        (y < 0) ? Mathf.Floor(y) * 1 * 1.5f + moveDirection.y * 18 : Mathf.Ceil(y) * 1.5f + moveDirection.y * 18,
        0);

       // bulletPrefab.transform.localScale = new Vector3(2, 2, 0);
        bulletControl = player.GetComponent<BulletControl>();


        if (uiInventory.fireAmount == 1)
        {
            bulletPrefab.transform.localScale = new Vector3(1f, 2f, 0);
            if (Input.GetKey("right"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletRight, transform.rotation) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("up"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletUp, upSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("left"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletLeft, leftSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("down"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletDown, downSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }
        }

        if (uiInventory.fireAmount == 2)
        {
            bulletPrefab.transform.localScale = new Vector3(2, 2, 0);

            if (Input.GetKey("right"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletRight, transform.rotation) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("up"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletUp, upSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if(Input.GetKey("left"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletLeft, leftSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

          else if (Input.GetKey("down"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletDown, downSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }
        }

        if (uiInventory.fireAmount == 3)
        {
            bulletPrefab.transform.localScale = new Vector3(4, 4, 0);

            if (Input.GetKey("right"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletRight, transform.rotation) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("up"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletUp, upSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("left"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletLeft, leftSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("down"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletDown, downSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }
        }


    }

    public void ShootWater(float x, float y)
    {
        userAnimator.SetTrigger("MeleeAttack");
        userAnimator.SetFloat("BulletHorizontal", x);
        userAnimator.SetFloat("BulletVertical", y);

       
        flameBulletRight = new Vector3(transform.position.x + 5, transform.position.y);
        flameBulletLeft = new Vector3(transform.position.x - 5, transform.position.y);
        flameBulletUp = new Vector3(transform.position.x, transform.position.y + 5);
        flameBulletDown = new Vector3(transform.position.x, transform.position.y - 5);

        upSprite = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90);
        downSprite = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 270);
        leftSprite = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 180);

        fireDir = new Vector3(
        (x < 0) ? Mathf.Floor(x) * 1 * 1.5f + moveDirection.x * 18 : Mathf.Ceil(x) * 1.5f + moveDirection.x * 18,
        (y < 0) ? Mathf.Floor(y) * 1 * 1.5f + moveDirection.y * 18 : Mathf.Ceil(y) * 1.5f + moveDirection.y * 18,
        0);

        // bulletPrefab.transform.localScale = new Vector3(2, 2, 0);
        bulletControl = player.GetComponent<BulletControl>();
        bulletPrefab.transform.localScale = new Vector3(1, 1, 0);

        if (uiInventory.waterAmount == 1)
        {
            if (Input.GetKey("right"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletRight, transform.rotation) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("up"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletUp, upSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("left"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletLeft, leftSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("down"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletDown, downSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }
        }

        if (uiInventory.waterAmount == 2)
        {
            bulletPrefab.transform.localScale = new Vector3(1.7f, 1.5f, 0);
            if (Input.GetKey("right"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletRight, transform.rotation) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("up"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletUp, upSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("left"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletLeft, leftSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("down"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletDown, downSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }
        }

        if (uiInventory.waterAmount == 3)
        {
            bulletPrefab.transform.localScale = new Vector3(2.5f, 2f, 0);
            if (Input.GetKey("right"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletRight, transform.rotation) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("up"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletUp, upSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("left"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletLeft, leftSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }

            else if (Input.GetKey("down"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, flameBulletDown, downSprite) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = fireDir;
            }
        }



    }

    public void ShootEarth(float x, float y)
    {
        userAnimator.SetTrigger("MeleeAttack");
        userAnimator.SetFloat("BulletHorizontal", x);
        userAnimator.SetFloat("BulletVertical", y);

        topBullet = new Vector3(transform.position.x + 1, transform.position.y + 1);
        bottomBullet = new Vector3(transform.position.x - 1, transform.position.y - 1);

        newKnife = Quaternion.Euler(0, 0, 180f) * knifeDir;
        newKnife2 = Quaternion.Euler(0, 0, 90f) * knifeDir;
        newKnife3 = Quaternion.Euler(0, 0, 270f) * knifeDir;

        knifeDir = new Vector3(
        (x < 0) ? Mathf.Floor(x) * 1 * bulletSpeed2 + moveDirection.x * 13 : Mathf.Ceil(x) * bulletSpeed2 + moveDirection.x * 13,
        (y < 0) ? Mathf.Floor(y) * 1 * bulletSpeed2 + moveDirection.y * 13 : Mathf.Ceil(y) * bulletSpeed2 + moveDirection.y * 13,
        0);

        bulletPrefab.transform.localScale = new Vector3(0.3f, 0.3f, 0);
        bulletControl = player.GetComponent<BulletControl>();
        sideBullet = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90);


        if (uiInventory.earthAmount == 1)
        {

            if (Input.GetKey("down") || Input.GetKey("up"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, transform.position, sideBullet) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = knifeDir;
            }

            else if (Input.GetKey("left") || Input.GetKey("right"))
            {
                GameObject bulletMelee = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                bulletMelee.GetComponent<Rigidbody2D>().velocity = knifeDir;
            }
        }

        if (uiInventory.earthAmount == 2)
        {
            //   bulletPrefab.transform.localScale = new Vector3(13, 13, 0);
            GameObject bulletMelee = Instantiate(bulletPrefab, topBullet, transform.rotation) as GameObject;
            bulletMelee.GetComponent<Rigidbody2D>().velocity = knifeDir;

            GameObject bulletMelee2 = Instantiate(bulletPrefab, bottomBullet, transform.rotation) as GameObject;
            bulletMelee2.GetComponent<Rigidbody2D>().velocity = knifeDir;

        }

        if (uiInventory.earthAmount == 3)
        {
            //   bulletPrefab.transform.localScale = new Vector3(13, 13, 0);
            GameObject bulletMelee = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bulletMelee.GetComponent<Rigidbody2D>().velocity = knifeDir;

            GameObject bulletMelee2 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bulletMelee2.GetComponent<Rigidbody2D>().velocity = newKnife;

            GameObject bulletMelee3 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bulletMelee3.GetComponent<Rigidbody2D>().velocity = newKnife2;

            GameObject bulletMelee4 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bulletMelee4.GetComponent<Rigidbody2D>().velocity = newKnife3;

        }

        if (uiInventory.earthAmount > 3)
        {
            //   bulletPrefab.transform.localScale = new Vector3(13, 13, 0);
            GameObject bulletMelee = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bulletMelee.GetComponent<Rigidbody2D>().velocity = knifeDir;

            GameObject bulletMelee2 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bulletMelee2.GetComponent<Rigidbody2D>().velocity = newKnife;

            GameObject bulletMelee3 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bulletMelee3.GetComponent<Rigidbody2D>().velocity = newKnife2;

            GameObject bulletMelee4 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            bulletMelee4.GetComponent<Rigidbody2D>().velocity = newKnife3;

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

        topBullet = new Vector3(transform.position.x + 1, transform.position.y + 1);
        bottomBullet = new Vector3(transform.position.x - 1, transform.position.y - 1);

        newBulletRotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 50);
        newBullet2 = Quaternion.Euler(0, 0, 30f) * bulletDir;
        newBullet3 = Quaternion.Euler(0, 0, -30f) * bulletDir;
       // sideBullet =  Quaternion.Euler(30f, 0, 0) * bulletDir;
        bottomBulletVel = Quaternion.Euler(0, 0, 0) * bulletDir;
        sideBullet = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 90);

        bulletControl = player.GetComponent<BulletControl>();

        bulletDir = new Vector3(
        (x < 0) ? Mathf.Floor(x) * 1 * bulletSpeed + moveDirection.x * 13 : Mathf.Ceil(x) * bulletSpeed + moveDirection.x * 13,
        (y < 0) ? Mathf.Floor(y) * 1 * bulletSpeed + moveDirection.y * 13 : Mathf.Ceil(y) * bulletSpeed + moveDirection.y * 13,
        0);


        bulletPrefab.transform.localScale = new Vector3(0.2f, 0.2f, 0);
        if (uiInventory.supremeGunAmount == 1)
        {
           
           

            if (Input.GetKey("down") || Input.GetKey("up"))
            {

                GameObject bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                //  bulletControl = player.GetComponent<BulletControl>();
                bullet2.GetComponent<Rigidbody2D>().velocity = bulletDir;
            }

           else if (Input.GetKey("right") || Input.GetKey("left"))
            {

                GameObject bullet = Instantiate(bulletPrefab, transform.position, sideBullet) as GameObject;
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDir;
            }
            
         
           



        }

        if (uiInventory.supremeGunAmount == 2)
        {

            if (Input.GetKey("down") || Input.GetKey("up"))
            {
                GameObject top = Instantiate(bulletPrefab, bottomBullet, sideBullet) as GameObject;
                //  bulletControl = player.GetComponent<BulletControl>();
                top.GetComponent<Rigidbody2D>().velocity = bulletDir;

                GameObject bottom = Instantiate(bulletPrefab, topBullet, sideBullet) as GameObject;
                //  bulletControl = player.GetComponent<BulletControl>();
                bottom.GetComponent<Rigidbody2D>().velocity = bulletDir;
            }


           else  if (Input.GetKey("down") || Input.GetKey("up"))
            {
                GameObject top = Instantiate(bulletPrefab, bottomBullet, transform.rotation) as GameObject;
                //  bulletControl = player.GetComponent<BulletControl>();
                top.GetComponent<Rigidbody2D>().velocity = bulletDir;

                GameObject bottom = Instantiate(bulletPrefab, topBullet, transform.rotation) as GameObject;
                //  bulletControl = player.GetComponent<BulletControl>();
                bottom.GetComponent<Rigidbody2D>().velocity = bulletDir;

            }

        }

        if (uiInventory.supremeGunAmount == 3)
        {

            if (Input.GetKey("down") || Input.GetKey("up"))
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                //  bulletControl = player.GetComponent<BulletControl>();
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDir;


                GameObject bullet2 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                bullet2.GetComponent<Rigidbody2D>().velocity = newBullet2;


                GameObject bullet3 = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
                bullet3.GetComponent<Rigidbody2D>().velocity = newBullet3;
            }

            else if (Input.GetKey("right") || Input.GetKey("left"))
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, sideBullet) as GameObject;
                //  bulletControl = player.GetComponent<BulletControl>();
                bullet.GetComponent<Rigidbody2D>().velocity = bulletDir;


                GameObject bullet2 = Instantiate(bulletPrefab, transform.position, sideBullet) as GameObject;
                bullet2.GetComponent<Rigidbody2D>().velocity = newBullet2;


                GameObject bullet3 = Instantiate(bulletPrefab, transform.position, sideBullet) as GameObject;
                bullet3.GetComponent<Rigidbody2D>().velocity = newBullet3;
            }
        }


        if (uiInventory.supremeGunAmount > 3)
        {

            if (Input.GetKey("down") || Input.GetKey("up"))
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
            else if (Input.GetKey("right") || Input.GetKey("left"))
            {

                GameObject top = Instantiate(bulletPrefab, bottomBullet, sideBullet) as GameObject;
                //  bulletControl = player.GetComponent<BulletControl>();
                top.GetComponent<Rigidbody2D>().velocity = bulletDir;

                GameObject bottom = Instantiate(bulletPrefab, topBullet, sideBullet) as GameObject;
                //  bulletControl = player.GetComponent<BulletControl>();
                bottom.GetComponent<Rigidbody2D>().velocity = bulletDir;


                GameObject bullet2 = Instantiate(bulletPrefab, transform.position, sideBullet) as GameObject;
                bullet2.GetComponent<Rigidbody2D>().velocity = newBullet2;


                GameObject bullet3 = Instantiate(bulletPrefab, transform.position, sideBullet) as GameObject;
                bullet3.GetComponent<Rigidbody2D>().velocity = newBullet3;
            }
        }

     /*   if (uiInventory.supremeGunAmount >4 )
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
     */
    }



    public void ShootAir(float x, float y)
    {
        userAnimator.SetTrigger("BottleAttack");
        userAnimator.SetFloat("BulletHorizontal", x);
        userAnimator.SetFloat("BulletVertical", y);



        //  GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;

        bulletControl = player.GetComponent<BulletControl>();

        newSpray2 = Quaternion.Euler(0, 0, 180f) * sprayDir2;

        topSpray = new Vector3(transform.position.x, transform.position.y + 1);
        bottomSpray = new Vector3(transform.position.x, transform.position.y - 1);


        sprayDir = new Vector3(
       (x < 0) ? Mathf.Floor(x) * 1 * airSpeed + moveDirection.x * 13 : Mathf.Ceil(x) * airSpeed + moveDirection.x * 13,
        (y < 0) ? Mathf.Floor(y) * 1 * airSpeed + moveDirection.y * 13 : Mathf.Ceil(y) * airSpeed + moveDirection.y * 13,
        0);

        sprayDir2 = new Vector3(
       (x < 0) ? Mathf.Floor(x) * 1 * bulletSpeed4 + moveDirection.x * 17 : Mathf.Ceil(x) * bulletSpeed4 + moveDirection.x * 17,
        (y < 0) ? Mathf.Floor(y) * 1 * bulletSpeed4 + moveDirection.y * 17 : Mathf.Ceil(y) * bulletSpeed4 + moveDirection.y * 17,
        0);

        if (uiInventory.airAmount == 1)
        {
            SprayDelay = 0.7f;
            airSpeed = 17;
            bulletPrefab.transform.localScale = new Vector3(1, 1, 0);
            GameObject spray = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            spray.GetComponent<Rigidbody2D>().velocity = sprayDir;
        }

        if (uiInventory.airAmount == 2)
        {
            SprayDelay = 0.5f;
            airSpeed = 20;
            bulletPrefab.transform.localScale = new Vector3(1.3f, 1.3f, 0);
            GameObject spray = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            spray.GetComponent<Rigidbody2D>().velocity = sprayDir;



        }

        if (uiInventory.airAmount == 3)
        {

            airSpeed = 21;
            SprayDelay = 0.3f;
            bulletPrefab.transform.localScale = new Vector3(2, 2, 0);
            GameObject spray = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            spray.GetComponent<Rigidbody2D>().velocity = sprayDir;
        }

        if (uiInventory.airAmount == 4)
        {
            SprayDelay = 0.2f;
            bulletPrefab.transform.localScale = new Vector3(2.5f, 2.5f, 0);
            GameObject spray = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;
            spray.GetComponent<Rigidbody2D>().velocity = sprayDir;
        }


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





        if (collision.gameObject.tag == "firstBoss" || collision.gameObject.tag == "secondBoss1"||
            collision.gameObject.tag == "secondBoss2" || collision.gameObject.tag == "thirdBoss" )
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            //Remove if we want player health
            Vector2 difference = transform.position - collision.transform.position;
            rb.velocity = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
            playerHealth--;
            rb.MovePosition(rb.velocity);

           

            Instantiate(particleScript, transform.position, Quaternion.identity);
            //enemyAI.PlayerHit();
            if (playerHealth < 1)
            {
                respawn();
                enemy.transform.position = new Vector2(-1434.1f, 963f);
            }


        }


        if (collision.gameObject.tag == "BossBullet")
        {
            // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

            //Remove if we want player health
            Vector2 difference = transform.position - collision.transform.position;
            rb.velocity = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
            playerHealth--;
            rb.MovePosition(rb.velocity);

            Instantiate(particleScript, transform.position, Quaternion.identity);
            //print(playerHealth);
            //enemyAI.PlayerHit();
            if (playerHealth < 1)
            {
                respawn();
                enemy.transform.position = new Vector2(-1434.1f, 963f);
            }


        }


        if (collision.gameObject.tag == "level2start")
        {
            inlevel2 = true;
            inlevel3 = false;
            print("123wefouiwjeaf");
        }

        if (collision.gameObject.tag == "Level3Start")
        {
            inlevel2 = false;
            inlevel3 = true;
            print("wefouiwjeaf");

        }

        if (collision.gameObject.tag == "Level3Exit")
        {
            inlevel2 = false;
            inlevel3 = false;
            finallevel = true;
            print("collision");

        }

        if (collision.gameObject.tag == "Enemy")
        {


            //Remove if we want player health
            Vector2 difference = transform.position - collision.transform.position;
            rb.velocity = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
            playerHealth--;
            rb.MovePosition(rb.velocity);
            // print(playerHealth);

            Instantiate(particleScript, transform.position, Quaternion.identity);
            //enemyAI.PlayerHit();

            if (playerHealth < 1)
            {
                respawn();

            }

        }

        if (playerHealth < 1)
        {
           // enemy.ResetBossPosition();
        }
    }

    public void respawn()
    {
       
            if (inlevel2 == true)
            {
                transform.position = new Vector2(-1457, -275);
                print("respawn");
                playerHealth = 3;
            }
            else if (inlevel3 == true)
            {
                transform.position = new Vector2(-1393, 419);
                print("respawn");
                playerHealth = 3;
            }

            else if (inlevel2 == false && inlevel3 == false)
            {
                 SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            playerHealth = 3;
        }
            // playerHealth = 3;
        } 
    
}
