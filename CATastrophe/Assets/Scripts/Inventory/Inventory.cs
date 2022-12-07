using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Inventory : MonoBehaviour
{
    public event EventHandler OnWeaponListChanged;
    private List<WeaponItem> weaponList;

    public Inventory()
    {
        weaponList = new List<WeaponItem>();


        //for testing -> can delete after
        AddWeapon(new WeaponItem { weaponType = WeaponItem.WeaponType.SupremeGun, amount = 1 });
        AddWeapon(new WeaponItem { weaponType = WeaponItem.WeaponType.Flamethrower, amount = 1 });
        AddWeapon(new WeaponItem { weaponType = WeaponItem.WeaponType.SprayBottle, amount = 1 });
        AddWeapon(new WeaponItem { weaponType = WeaponItem.WeaponType.Stick, amount = 1 });

        Debug.Log(weaponList.Count);

        //end testing code
    }

    void Update()
    {
       // DontDestroyOnLoad(this);
       // DontDestroyOnLoad(transform);
       // DontDestroyOnLoad(weaponList);
          }

        public void AddWeapon(WeaponItem weaponItem)
    {
        if (weaponItem.IsStackable())
        {
            bool weapponAlreadyInInvetory = false;
            foreach (WeaponItem inventoryWeapon in weaponList)
            {
                if (inventoryWeapon.weaponType == weaponItem.weaponType)
                {
                    inventoryWeapon.amount += weaponItem.amount;
                    weapponAlreadyInInvetory = true;
                }
            }
            if (!weapponAlreadyInInvetory)
            {
                weaponList.Add(weaponItem);
            }
        } else {
            weaponList.Add(weaponItem);
        }


    
        OnWeaponListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<WeaponItem> GetWeaponList()
    {
        return weaponList;
    }

}
