using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    int totalWeapons = 1;
    public int currentWeaponIndex;

    public GameObject[] guns;
    public GameObject weaponHolder;
    public GameObject currentGun;
    public string A = "Arrow Variant";

    BulletControl bulletControl;
    WeaponSwitch weaponSwitch;
   public GameObject player;
    public GameObject player2;
    public GameObject bulletPrefab;

    GameObject Bullet;
 
    new Rigidbody2D rigidbody;
   // public Text collectedText;
    public static int collectedAmount = 0;

    //  public GameObject bulletPrefab;

    public float bulletSpeed;
    public float bulletSpeed2;
    public float lastFire;
    private float fireDelay;

    // Start is called before the first frame update
    void Start()
    {
    
        rigidbody = GetComponent<Rigidbody2D>();

        totalWeapons = weaponHolder.transform.childCount;
        guns = new GameObject[totalWeapons];

        for (int i = 0; i < totalWeapons; i++)
        {
            guns[i] = weaponHolder.transform.GetChild(i).gameObject;
            guns[i].SetActive(false);

        }

       bulletControl = player.GetComponent<BulletControl>();

      weaponSwitch = player2.GetComponent<WeaponSwitch>();

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
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

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
            if (weaponSwitch.WeaponBow == true)
            {
                fireDelay = 0.5f;
                Shoot2(shootHor, shootVert);
               
            }
            lastFire = Time.time;
         //   print(bulletPrefab);
       //     print("1+1");
   // print(bulletPrefab);
        }

       
            rigidbody.velocity = new Vector3(horizontal * 5, vertical * 5, 0);
     
        //  collectedText.text = "Items Collected: " + collectedAmount;

    /*    if (Input.GetKeyDown(KeyCode.J))
        {

            if (currentWeaponIndex < totalWeapons - 1)
            {
                print("bye");
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex += 1;
                guns[currentWeaponIndex].SetActive(true);
                currentGun = guns[currentWeaponIndex];
            }

        }

        if (Input.GetKeyDown(KeyCode.K))
        {

            if (currentWeaponIndex > 0)
            {
                print("hello");
                guns[currentWeaponIndex].SetActive(false);
                currentWeaponIndex -= 1;
                guns[currentWeaponIndex].SetActive(true);
                currentGun = guns[currentWeaponIndex];
            }

        }
    */



    }

    public void SetBulletPreFab(GameObject newBullet)
    {
        bulletPrefab = newBullet;
       
    }

    public void GainWeapon()
    {
        Destroy(gameObject);
    }

    public void Shoot2(float x, float y)
    {




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
      
        

       
            GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation) as GameObject;

            bulletControl = player.GetComponent<BulletControl>();
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(



            (x < 0) ? Mathf.Floor(x) * bulletSpeed : Mathf.Ceil(x) * bulletSpeed,
            (y < 0) ? Mathf.Floor(y) * bulletSpeed : Mathf.Ceil(y) * bulletSpeed,
            0
        );
        
      


        /*

        if (currentGun = guns[1])
        {

            GameObject bullet = Instantiate(currentGun, transform.position, transform.rotation) as GameObject;



            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(



                (x < 0) ? Mathf.Floor(x) * 1 : Mathf.Ceil(x) * 1,
                (y < 0) ? Mathf.Floor(y) * 5 : Mathf.Ceil(y) * 1,
                0
            );

        }

        else if (currentGun = guns[0])
        {

            GameObject bullet = Instantiate(currentGun, transform.position, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                (x < 0) ? Mathf.Floor(x) * 1 : Mathf.Ceil(x) * 1,
                (y < 0) ? Mathf.Floor(y) * 1 : Mathf.Ceil(y) * 1,
                0
            );

        }

        else if (currentGun = guns[2])
        {
            GameObject bullet = Instantiate(currentGun, transform.position, transform.rotation) as GameObject;
            bullet.GetComponent<Rigidbody2D>().velocity = new Vector3(
                (x < 0) ? Mathf.Floor(x) * 1 : Mathf.Ceil(x) * 1,
                (y < 0) ? Mathf.Floor(y) * 1 : Mathf.Ceil(y) * 1,
                0
            );

        }
    }
        */
    }


}
