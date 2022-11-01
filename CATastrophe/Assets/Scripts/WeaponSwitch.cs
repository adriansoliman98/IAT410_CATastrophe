using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour
{
    [SteralizedField] public GameObject cashgun;
    [SteralizedField] public GameObject bow;
    [SteralizedField] private float currentGun;

    public bool WeaponCash = true;
    public bool WeaponBow = false;


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
            WeaponBow = false;
            currentGun = 1;
            SetWeapon(1);
            print(WeaponCash);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {

         //   if (bowInventory == true)
        //    {
                WeaponCash = false;
                WeaponBow = true;
                currentGun = 2;
                print(WeaponCash);
                SetWeapon(2);
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
                playerController.SetBulletPreFab(bow);
            
                break;

        }
    }
}
