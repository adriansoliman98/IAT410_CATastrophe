using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWorld : MonoBehaviour
{

    private WeaponItem weaponItem;
    private SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    public static WeaponWorld SpawnWeaponWorld(Vector2 position, WeaponItem weaponItem)
    {
       Transform transform = Instantiate(WeaponAssets.Instance.pfWeaponWorld, position, Quaternion.identity);

        WeaponWorld weaponWorld = transform.GetComponent<WeaponWorld>();
        weaponWorld.SetWeapon(weaponItem);

        return weaponWorld;
    }


    public void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetWeapon(WeaponItem weaponItem)
    {
        this.weaponItem = weaponItem;
        spriteRenderer.sprite = weaponItem.GetSprite();
    }

    public WeaponItem GetWeapon()
    {
        return weaponItem;
    }

    public void DestorySelf()
    {
        Destroy(gameObject);
    }



}
