using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SteralizedField] public GameObject cashgun;
    [SteralizedField] public GameObject air;
    [SteralizedField] public GameObject earth;
    [SteralizedField] public GameObject fire;
    [SteralizedField] public GameObject water;
    [SteralizedField] private float currentGun;

    public bool WeaponCash = true;
    public bool WeaponAir = false;
    public bool WeaponEarth = false;
    public bool WeaponWater = false;
    public bool WeaponFire = false;

    PlayerController playerController;

   // int totalWeapons = 2;
   // public int currentWeaponIndex;

   // public GameObject[] guns;
    //public GameObject weaponHolder;
    //  public GameObject currentGun;
    // Start is called before the first frame update

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {

        WeaponCash = true;
        WeaponAir = false;
        WeaponEarth = false;
        WeaponFire = false;
        WeaponWater = false;
        currentGun = 1;
        SetWeapon(1);
    }



    // Update is called once per frame
    void Update()
    {

      

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponCash = true;
            WeaponAir = false;
            WeaponEarth = false;
            WeaponFire = false;
            WeaponWater = false;
            currentGun = 1;
            SetWeapon(1);

        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

            // if (bowInventory == true)
            {
                WeaponCash = false;
                WeaponAir = true;
                WeaponEarth = false;
                WeaponFire = false;
                WeaponWater = false;
                currentGun = 2;
              
                SetWeapon(2);
            }


        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            //   if (bowInventory == true)
            {
                WeaponCash = false;
                WeaponAir = false;
                WeaponEarth = true;
                WeaponFire = false;
                WeaponWater = false;
                currentGun = 3;

                SetWeapon(3);
            }


        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {

            //   if (bowInventory == true)
            {
                WeaponCash = false;
                WeaponAir = false;
                WeaponEarth = false;
                WeaponFire = true;
                WeaponWater = false;
                currentGun = 4;

                SetWeapon(4);
            }


        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {

            //   if (bowInventory == true)
            {
                WeaponCash = false;
                WeaponAir = false;
                WeaponEarth = false;
                WeaponFire = false;
                WeaponWater = true;
                currentGun = 5;

                SetWeapon(5);
            }


        }




    }

    void SetWeapon(int weaponID)
    {
    /*    if (currentGun == 1)
        {
            playerController.SetBulletPreFab(cashgun);
        }
        else if (currentGun == 2)
        {

            playerController.SetBulletPreFab(bow);

        }
    */
        switch (weaponID)
        {
            case 1:
                playerController.SetBulletPreFab(cashgun);

                break;
            case 2:
                playerController.SetBulletPreFab(air);

                break;

            case 3:
                playerController.SetBulletPreFab(earth);

                break;

            case 4:
                playerController.SetBulletPreFab(fire);

                break;

            case 5:
                playerController.SetBulletPreFab(water);

                break;



        }
    }
}
