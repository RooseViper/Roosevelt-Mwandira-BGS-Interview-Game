using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the Items and Inventory of the Player. Note: This script is using the Singleton Design pattern.
/// </summary>
public class InventoryManager : MonoBehaviour
{
    public OutFitManager outFitManager;
    public Text generalDisplayBalanceText;
    public float balance;
    public RectTransform inventoryItemsParent;
    public static InventoryManager Instance => _instance;
    private static InventoryManager _instance;
    [HideInInspector]
    public InventoryItem[] inventoryItems;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        generalDisplayBalanceText.text = "" + balance;
        inventoryItems = inventoryItemsParent.GetComponentsInChildren<InventoryItem>();
        foreach (InventoryItem inventoryItem in inventoryItems)
        {
            inventoryItem.Clear();
        }
    }


    public void OpenInventory()
    {
        ShopItem[] shopItems = ShopManager.Instance.shopItemsPlayerParent.GetComponentsInChildren<ShopItem>();
        List<ShopItem> newShopItems = new List<ShopItem>();
        foreach (ShopItem shopItem in shopItems)
        {
            if (!string.IsNullOrEmpty(shopItem.itemName))
            {
                newShopItems.Add(shopItem);
            }
        }
        for (int i = 0; i < newShopItems.Count; i++)
        {
            if (newShopItems[i].itemName != null)
            {
                inventoryItems[i].image.enabled = true;
                inventoryItems[i].itemName = newShopItems[i].itemName;
                inventoryItems[i].outfit = newShopItems[i].outfit;
                if (newShopItems[i].price <= 0f)
                {
                    inventoryItems[i].isOutfit = true;
                }
                inventoryItems[i].image.sprite = newShopItems[i].sprite;
                inventoryItems[i].quantityText.text = ""+ newShopItems[i].quantity;
            }
        }
        GameManager.Instance.OpenCloseCanvas("CanvasInventory", true);
        AudioController.Instance.Play("Inventory Open");
    }



    public void CloseInventory()
    {
        foreach (InventoryItem inventoryItem in inventoryItems)
        {
            inventoryItem.Clear();
        }
        GameManager.Instance.OpenCloseCanvas("CanvasInventory", false);
        AudioController.Instance.Play("Inventory Close");
    }
    
    /// <summary>
    /// Picks up an Item from the ground
    /// </summary>
    public void PickupItem(string itemName)
    {
        ShopItem[] shop_Items = ShopManager.Instance.shopItemsPlayerParent.GetComponentsInChildren<ShopItem>();
        Item item = Array.Find(ShopManager.Instance.shopInventory.items, i => i.itemName == itemName);
        ShopItem shopItem = Array.Find(shop_Items, s => s.itemName == itemName);
        if (item != null && shopItem != null)
        {
            shopItem.quantity++;
            shopItem.Initialize();
        }
        AudioController.Instance.Play("Pickup");
    }
}
