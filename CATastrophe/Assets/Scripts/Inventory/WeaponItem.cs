using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WeaponItem
{
    public enum WeaponType
    {
        SupremeGun,
        Flamethrower,
        SprayBottle,
        Stick,
        Nothing,
        Heart,
    }


    public WeaponType weaponType;
    public int amount;


    public Sprite GetSprite()
    {
        switch (weaponType)
        {
            default:
            case WeaponType.SupremeGun:      return WeaponAssets.Instance.supremeGunSprite;
            case WeaponType.Flamethrower:    return WeaponAssets.Instance.flamethrowerSprite;
            case WeaponType.SprayBottle:     return WeaponAssets.Instance.sprayBottleSprite;
            case WeaponType.Stick:           return WeaponAssets.Instance.stickSprite;
            case WeaponType.Nothing:         return WeaponAssets.Instance.nothing;
            case WeaponType.Heart:           return WeaponAssets.Instance.heartSprite;

        }
    }

    public bool IsStackable() {
        switch (weaponType) {
            default:
            case WeaponType.SupremeGun:
            case WeaponType.Flamethrower:
            case WeaponType.SprayBottle:
          
            case WeaponType.Stick:
                return true;
          
           
                return false;
        }
    }

}
