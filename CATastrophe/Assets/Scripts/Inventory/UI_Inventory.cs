using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static WeaponItem;

public class UI_Inventory : MonoBehaviour
{
    public bool gunShoot = false;
    private Inventory inventory;
    private Transform weaponSlotContainer;
    private Transform weaponSlotTemplate;
    public int knifeAmount;
    public int sprayBottleAmount;
    public int supremeGunAmount;
    public int minecraftBowAmount;
    public int heartAmount;
    // public TextMeshProUGUI text;

    private void Awake()
    {
        weaponSlotContainer = transform.Find("weaponSlotContainer");
        weaponSlotTemplate = weaponSlotContainer.Find("weaponSlotTemplate");
       // text = GetComponent<TextMeshProUGUI>();

    }

     void Update()
    {
        DontDestroyOnLoad(weaponSlotContainer);
        DontDestroyOnLoad(weaponSlotTemplate);
        DontDestroyOnLoad(weaponSlotTemplate.gameObject);
        DontDestroyOnLoad(this);
        DontDestroyOnLoad(gameObject);
       // DontDestroyOnLoad(inventory);
    }

    public void SetInventory(Inventory inv)
    {
        this.inventory = inv;

        inventory.OnWeaponListChanged += Invetory_OnWeaponListChanged;

        RefreshInventoryWeapons();
    }

    private void Invetory_OnWeaponListChanged (object sender, System.EventArgs e)
    {
        RefreshInventoryWeapons();
    }

    public void RefreshInventoryWeapons()
    {
        foreach (Transform child in weaponSlotContainer)
        {
            if (child == weaponSlotTemplate) continue;
            Destroy(child.gameObject);
        }


        int x = 0;
        int y = 0;
        float inventorySlotCellSize = 120f;

        //for every weapon in the list of weapons(inventory)
        foreach (WeaponItem weapon in inventory.GetWeaponList())
        {
            //instantiate our template
            RectTransform weaponSlotRectTransform = Instantiate(weaponSlotTemplate, weaponSlotContainer).GetComponent<RectTransform>();
            //set the template to active
            weaponSlotRectTransform.gameObject.SetActive(true);
            //position the item in a grid
            weaponSlotRectTransform.anchoredPosition = new Vector2(x * inventorySlotCellSize, y * inventorySlotCellSize);
            //retrieve the correct sprite
            Image image = weaponSlotRectTransform.Find("image").GetComponent<Image>();
            image.sprite = weapon.GetSprite();

            TextMeshProUGUI uiText = weaponSlotRectTransform.Find("text").GetComponent<TextMeshProUGUI>();
            if (weapon.amount > 1)
            {
                uiText.SetText(weapon.amount.ToString());
            } else {
               uiText.SetText(" ");
            }

         //   print(weapon.amount);
          //  print(weapon.weaponType);


            if (weapon.weaponType == WeaponType.Flamethrower)
            {
                knifeAmount = weapon.amount ;
                
            }

            if (weapon.weaponType == WeaponType.SupremeGun)
            {
                supremeGunAmount = weapon.amount;

            }
            if (weapon.weaponType == WeaponType.SprayBottle)
            {
                sprayBottleAmount = weapon.amount;

            }
            if (weapon.weaponType == WeaponType.Stick)
            {
                minecraftBowAmount = weapon.amount;

            }

            if (weapon.weaponType == WeaponType.Heart)
            {
                heartAmount = weapon.amount;

            }

          


            x++;
            if (x > 5)
            {
                x = 0;
                y++;
            }
        }
    }
}
