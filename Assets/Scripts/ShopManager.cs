using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    /// <summary>
    /// Scriptable object storing all the Items in the shop
    /// </summary>
    public ShopInventory shopInventory;
    public GameObject shopItemsParent;
    public GameObject popUpErrorGameobject;
    public Sprite[] previewOutfitSprites;
    public OutFitManager outfitManager;
    public Canvas canvas;
    public GameObject buyAndEquipButton;
    public Text selectedShopItemName;
    public Image selectedShopItemImage;
    public Text selectedShopItemDescription;
    public Text selectedShopItemPrice;
    public ShopItem selectedShopItem;
    [HideInInspector]
    public List<ShopItem> shopItems;

    private float _balance;
    public static  ShopManager Instance => _instance;
    private static ShopManager _instance;
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        shopItems = shopItemsParent.GetComponentsInChildren<ShopItem>().ToList();
        _balance = shopInventory.balance;
        StartCoroutine(LoadShopItems());
    }

    /// <summary>
    /// After launching, this loads the Items in the Shop
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadShopItems()
    {
        yield return new WaitForEndOfFrame();
        for (int i = 0; i < shopInventory.items.Length; i++)
        {
            shopItems[i].itemName = shopInventory.items[i].itemName;
            shopItems[i].outfit = shopInventory.items[i].outfit;
            shopItems[i].sprite = shopInventory.items[i].sprite;
            shopItems[i].description = shopInventory.items[i].description;
            shopItems[i].price = shopInventory.items[i].price;
            shopItems[i].quantity = shopInventory.items[i].quantity;
            shopItems[i].Initialize();
        }
        foreach (ShopItem shopItem in shopItems)
        {
            shopItem.ClearSlot();
        }
    }

    /// <summary>
    /// Previews the appropriate selected outfit.
    /// </summary>
    public void DisplayPreviewOutfit(Outfit.OutfitName outfitName)
    {;
        if (outfitName == Outfit.OutfitName.Red)
        {
            selectedShopItemImage.sprite = previewOutfitSprites[0];
        }
        else if(outfitName == Outfit.OutfitName.Galactic)
        {
            selectedShopItemImage.sprite = previewOutfitSprites[1];
        }else if (outfitName == Outfit.OutfitName.BlueGravityStudios)
        {
            selectedShopItemImage.sprite = previewOutfitSprites[2];
        }
    }
    /// <summary>
    /// Buys and Equips the outfit
    /// </summary>
    public void BuyAndEquip()
    {
        outfitManager.ChangeOutfit(selectedShopItem.outfit);
        Close();
    }

    public void Buy()
    {
        
    }

    /// <summary>
    /// This calculates the balance before making the purhcase
    /// </summary>
    private void CalculateBalances()
    {
        
    }

    public void Open()
    {
        canvas.enabled = true;
    }

    public void Close()
    {
        selectedShopItemName.text = "";
        buyAndEquipButton.SetActive(false);
        selectedShopItemImage.color = Color.clear;
        selectedShopItemDescription.text = "";
        selectedShopItemPrice.text = "";
        selectedShopItem = null;
        canvas.enabled = false;
    }
}
