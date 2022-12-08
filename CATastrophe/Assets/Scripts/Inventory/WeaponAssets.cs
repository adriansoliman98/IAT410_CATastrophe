using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAssets : MonoBehaviour
{
    public static WeaponAssets Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }
    public Transform pfWeaponWorld;

    public Sprite supremeGunSprite;
    public Sprite flamethrowerSprite;
    public Sprite sprayBottleSprite;
    public Sprite stickSprite;
    public Sprite airSprite;
    public Sprite nothing;
    public Sprite heartSprite;


}
