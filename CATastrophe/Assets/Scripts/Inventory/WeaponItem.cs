using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem
{
    public enum WeaponType
    {
        SupremeGun,
        Knife,
        SprayBottle,
        MinecraftBow,
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

        }
    }
}
