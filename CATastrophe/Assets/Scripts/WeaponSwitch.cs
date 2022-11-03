using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SteralizedField] public GameObject cashgun;
    [SteralizedField] public GameObject arrow;
    [SteralizedField] public GameObject melee;
    [SteralizedField] private float currentGun;

    public bool WeaponCash = true;
    public bool WeaponArrow = false;
    public bool WeaponMelee = false;

    PlayerController playerController;

    //  int totalWeapons = 2;
    //  public int currentWeaponIndex;

    //    public GameObject[] guns;
    //  public GameObject weaponHolder;
    //  public GameObject currentGun;
    // Start is called before the first frame update

    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
    }
    void Start()
    {



        //  totalWeapons = weaponHolder.transform.childCount;
        //   guns = new GameObject[totalWeapons];

        /*(
        8for (int i = 0; i < totalWeapons; i++)
           {
               guns[i] = weaponHolder.transform.GetChild(i).gameObject;
               guns[i].SetActive(false);

           }

           guns[0].SetActive(true);
           currentGun = guns[0];
           currentWeaponIndex = 0;

           */

    }



    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            WeaponCash = true;
            WeaponArrow = false;
            WeaponMelee = false;
            currentGun = 1;
            SetWeapon(1);
            
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

         //   if (bowInventory == true)
        //    {
                WeaponCash = false;
            WeaponArrow = false;
              WeaponMelee = true;
                currentGun = 2;
            print(WeaponMelee);
                SetWeapon(2);
         //   }
           

        }

        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            //   if (bowInventory == true)
            //    {
            WeaponCash = false;
            WeaponMelee = false;
            WeaponArrow = true;
            currentGun = 3;
         
            SetWeapon(3);
            //   }


        }




    }

    void SetWeapon(int weaponID)
    {
        /*  if (currentGun == 1) { 
          playerController.SetBulletPreFab(cashgun);
      }
          else if (currentGun == 2) { 

                  playerController.SetBulletPreFab(bow);

          }
      }*/
        switch (weaponID)
        {
            case 1:
                playerController.SetBulletPreFab(cashgun);
             
                break;
            case 2:
                playerController.SetBulletPreFab(melee);
            
                break;

            case 3:
                playerController.SetBulletPreFab(arrow);

                break;

        }
    }
}
