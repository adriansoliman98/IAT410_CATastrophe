using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Transform weaponSlotContainer;
    private Transform weaponSlotTemplate;

    private void Awake()
    {
        weaponSlotContainer = transform.Find("weaponSlotContainer");
        weaponSlotTemplate = weaponSlotContainer.Find("weaponSlotTemplate");
    }

    public void SetInventory(Inventory inv)
    {
        this.inventory = inv;
        RefreshInventoryWeapons();
    }

    private void RefreshInventoryWeapons()
    {
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
            x++;
            if (x > 5)
            {
                x = 0;
                y++;
            }
        }
    }
}
