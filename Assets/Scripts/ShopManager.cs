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
    public GameObject sellButton;
    public GameObject buyAndEquipButton;
    public Text selectedShopItemName;
    public Image selectedShopItemImage;
    public Text selectedShopItemDescription;
    public Text selectedShopItemPrice;
    public ShopItem selectedShopItem;
    [HideInInspector]
    public List<ShopItem> shopItems;
    private float _balance;
    private ShopSectionDisplaySelector _shopSectionDisplaySelector;
    public static bool OnBuyingPanel = false;
    public static  ShopManager Instance => _instance;
    private static ShopManager _instance;
    public ShopSectionDisplaySelector ShopSectionDisplaySelector { get => _shopSectionDisplaySelector; set => _shopSectionDisplaySelector = value; }
    private void Awake()
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        shopItems = shopItemsParent.GetComponentsInChildren<ShopItem>().ToList();
        _shopSectionDisplaySelector = GetComponent<ShopSectionDisplaySelector>();
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
        LoadShopInventory(shopItemsPlayerParent.GetComponentsInChildren<ShopItem>().ToList(), playerInventory);
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
        else if (outfitName == Outfit.OutfitName.Default)
        {
            selectedShopItemImage.sprite = previewOutfitSprites[3];
        }
    }
    
    /// <summary>
    /// Buys and Equips the outfit
    /// </summary>
    public void BuyAndEquip()
    {
        outfitManager.ChangeOutfit(selectedShopItem.outfit);
        BuyItem();
        Close(true);
    }

    /// <summary>
    /// If the SHop Keeper has This adds a new balance for the ShopKeeper and subtracts the bought item price from the Player balance.
    /// It also adds the bought Item to the Players Inventory
    /// </summary>
    public void BuyItem()
    {
        //Checks if the Player has enough money
        if (InventoryManager.Instance.balance > selectedShopItem.price  && selectedShopItem.quantity > 0)
        {
            _balance += selectedShopItem.price;
            InventoryManager.Instance.balance -= selectedShopItem.price;
            //The selected item in the Shop Keepers inventory.
            ShopItem shopItem = Array.Find(shopItems.ToArray(), sh => sh.itemName == selectedShopItemName.text);
            //The bought Item in the Players inventory
            ShopItem shopItemPlayer = Array.Find(shopItemsPlayerParent.GetComponentsInChildren<ShopItem>(), sh => sh.itemName == selectedShopItemName.text);

            if (shopItem != null)
            {
                shopItem.UpdateItem(false);
            } 
            if (shopItemPlayer != null)
            {
                shopItemPlayer.UpdateItem(true);
            }
            UpdateBalances();
            AddBoughtItem();
        }
    }
    public void SellItem()
    {
        //Checks if the Shop Keeper has enough money
        if (_balance > selectedShopItem.price && selectedShopItem.quantity > 0)
        {
            _balance -= selectedShopItem.price;
            InventoryManager.Instance.balance += selectedShopItem.price;
            //The selected item being sold.
            ShopItem shopItem = Array.Find(shopItemsPlayerParent.GetComponentsInChildren<ShopItem>(), sh => sh.itemName == selectedShopItemName.text);
            //The sold Item bought by the Shop keeper
            ShopItem shopItemShopKeeper =  Array.Find(shopItems.ToArray(), sh => sh.itemName == selectedShopItemName.text);
            if (shopItem != null)
            {
                shopItem.UpdateItem(false);
            } 
            if (shopItemShopKeeper != null)
            {
                shopItemShopKeeper.UpdateItem(true);
            }
            UpdateBalances();
        }
    }
    
    private void AddBoughtItem()
    {
        List<ShopItem> currentShopItems = shopItemsPlayerParent.GetComponentsInChildren<ShopItem>().ToList();
        ShopItem newShopItem = Array.Find(currentShopItems.ToArray(), sh => sh.itemName == selectedShopItem.itemName);
        if (newShopItem == null)
        {
            int index = PlayerSlotsFilled(currentShopItems);
            currentShopItems[index].itemName = selectedShopItem.itemName;
            currentShopItems[index].outfit = selectedShopItem.outfit;
            currentShopItems[index].sprite = selectedShopItem.sprite;
            currentShopItems[index].description = selectedShopItem.description;
            currentShopItems[index].price = 0;
            currentShopItems[index].quantity = 1;
            currentShopItems[index].Initialize();
        }
    }

    private int PlayerSlotsFilled(List<ShopItem> _shopItems)
    {
        int counter = 0;
        foreach (ShopItem shopItem in _shopItems)
        {
            if (!string.IsNullOrEmpty(shopItem.itemName))
            {
                counter++;
            }
        }
        return counter;
    }

    /// <summary>
    /// Updates the balances for the Player and Shop Keeper
    /// </summary>
    private void UpdateBalances()
    {
        shopBalanceTextBuying.text = "" + _balance;
        playerBalanceTextBuying.text = "" + InventoryManager.Instance.balance;

        shopBalanceTextSelling.text = "" + _balance;
        playerBalanceTextSelling.text = "" + InventoryManager.Instance.balance;
        
        InventoryManager.Instance.generalDisplayBalanceText.text = "" + InventoryManager.Instance.balance;
    }

    public void Close(bool isBuying)
    {
        selectedShopItemName.text = "";
        buyAndEquipButton.SetActive(false);
        selectedShopItemImage.color = Color.clear;
        selectedShopItemDescription.text = "";
        selectedShopItemPrice.text = "";
        selectedShopItem = null;
        GameManager.Instance.OpenCloseCanvas("CanvasGeneral", true);
        GameManager.Instance.OpenCloseCanvas(isBuying ? "CanvasShopBuying" : "CanvasShopSelling", false);
    }
}
