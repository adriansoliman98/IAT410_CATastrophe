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
        Knife,
        SprayBottle,
        MinecraftBow,
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
            case WeaponType.Knife:           return WeaponAssets.Instance.knifeSprite;
            case WeaponType.SprayBottle:     return WeaponAssets.Instance.sprayBottleSprite;
            case WeaponType.MinecraftBow:    return WeaponAssets.Instance.minecraftBowSprite;
            case WeaponType.Nothing:         return WeaponAssets.Instance.nothing;
            case WeaponType.Heart:           return WeaponAssets.Instance.heartSprite;

        }
    }

    public bool IsStackable() {
        switch (weaponType) {
            default:
            case WeaponType.SupremeGun:
            case WeaponType.Knife:
            case WeaponType.SprayBottle:
            case WeaponType.Heart:
                return true;
            case WeaponType.MinecraftBow:
           
                return false;
        }
    }

}
