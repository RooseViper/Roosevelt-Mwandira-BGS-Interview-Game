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
    /// <summary>
    /// Scriptable object storing all the default Items in the players inventory 
    /// </summary>
    public ShopInventory playerInventory;
    public GameObject shopItemsParent;
    public GameObject shopItemsPlayerParent;
    /// <summary>
    /// Displays the balance of the Shop keeper when buying
    /// </summary>
    public Text shopBalanceTextBuying;
    /// <summary>
    /// Displays the balance of the Player when buying
    /// </summary>
    public Text playerBalanceTextBuying;
    /// <summary>
    /// Displays the balance of the Shop keeper when selling
    /// </summary>
    public Text shopBalanceTextSelling;
    /// <summary>
    /// Displays the balance of the Player when selling
    /// </summary>
    public Text playerBalanceTextSelling; 
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
    [HideInInspector]
    public List<ShopItem> shopItemsPlayer;
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
        shopItemsPlayer = shopItemsPlayerParent.GetComponentsInChildren<ShopItem>().ToList();
        _balance = shopInventory.balance;
        shopBalanceTextBuying.text = "" + _balance;
        shopBalanceTextSelling.text = "" + _balance;
        
        StartCoroutine(LoadShopItems());
    }

    /// <summary>
    /// After launching, this loads the Items in the Shop
    /// </summary>
    /// <returns></returns>
    IEnumerator LoadShopItems()
    {
        yield return new WaitForEndOfFrame();
        playerBalanceTextBuying.text = "" + InventoryManager.Instance.balance;
        playerBalanceTextSelling.text = "" + InventoryManager.Instance.balance;
        // Loads up the Items that can be bought
        LoadShopInventory(shopItems, shopInventory);
        //Loads up the Player Inventory in the Shop so the Items can be sold
        LoadShopInventory(shopItemsPlayer, playerInventory);
    }

    /// <summary>
    /// Sets up the Items that can be sold
    /// </summary>
    private void LoadShopInventory(List<ShopItem> currentShopItems, ShopInventory shopInventory)
    {
        for (int i = 0; i < shopInventory.items.Length; i++)
        {
            currentShopItems[i].itemName = shopInventory.items[i].itemName;
            currentShopItems[i].outfit = shopInventory.items[i].outfit;
            currentShopItems[i].sprite = shopInventory.items[i].sprite;
            currentShopItems[i].description = shopInventory.items[i].description;
            currentShopItems[i].price = shopInventory.items[i].price;
            currentShopItems[i].quantity = shopInventory.items[i].quantity;
            currentShopItems[i].Initialize();
        }
        foreach (ShopItem currentShopItem in currentShopItems)
        {
            currentShopItem.ClearSlot();
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
        Buy();
        Close();
    }

    public void Buy()
    {
        AddBalance();
    }

    public void Sell()
    {
        
    }

    /// <summary>
    /// This adds a new balance for the ShopKeeper and subtracts the bought item price from the Player balance 
    /// </summary>
    private void AddBalance()
    {
        if (InventoryManager.Instance.balance > selectedShopItem.price)
        {
            _balance += selectedShopItem.price;
            InventoryManager.Instance.balance -= selectedShopItem.price;
        }
        shopBalanceTextBuying.text = "" + _balance;
        playerBalanceTextBuying.text = "" + InventoryManager.Instance.balance;
        InventoryManager.Instance.generalDisplayBalanceText.text = "" + InventoryManager.Instance.balance;
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
