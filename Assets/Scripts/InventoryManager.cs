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
    }

    public void CloseInventory()
    {
        foreach (InventoryItem inventoryItem in inventoryItems)
        {
            inventoryItem.Clear();
        }
        GameManager.Instance.OpenCloseCanvas("CanvasInventory", false);
    }
}
