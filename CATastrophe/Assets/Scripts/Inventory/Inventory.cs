using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory 
{

    private List<WeaponItem> weaponList;

    public Inventory()
    {
        weaponList = new List<WeaponItem>();


        //for testing -> can delete after
        AddWeapon(new WeaponItem { weaponType = WeaponItem.WeaponType.MinecraftBow, amount = 1 });
        AddWeapon(new WeaponItem { weaponType = WeaponItem.WeaponType.SupremeGun, amount = 1 });
        AddWeapon(new WeaponItem { weaponType = WeaponItem.WeaponType.Knife, amount = 1 });

        Debug.Log(weaponList.Count);

        //end testing code
    }

    public void AddWeapon(WeaponItem weaponItem)
    {
        weaponList.Add(weaponItem);
    }

    public List<WeaponItem> GetWeaponList()
    {
        return weaponList;
    }

}
