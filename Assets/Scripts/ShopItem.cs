using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public string itemName;
    public Outfit.OutfitName outfit;
    public Sprite sprite;
    public string description;
    public float price;
    public int quantity;

    public Image image;
    public Text quantityText;
    public Text priceText;
    public Image coinsImage;

    private  Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SelectShopItem);
    }

    public void Initialize()
    {
        //Checks if an Item has been loaded
        if (!string.IsNullOrEmpty(itemName))
        {
            image.enabled = true;
            image.sprite = sprite;
            quantityText.text = "" + quantity;
            priceText.text = "" + price;
            coinsImage.enabled = true;
        }
    }

    /// <summary>
    /// Clears the Shop Item slot if the Slot is empty
    /// </summary>
    public void ClearSlot()
    {
        if (string.IsNullOrEmpty(itemName))
        {
            image.enabled = false;
            itemName = "";
            quantityText.text = "";
            priceText.text = "";
            coinsImage.enabled = false;
            
            //Removes the buttons transition effects
            var colors = _button.colors;
            colors.highlightedColor = Color.white;
            colors.pressedColor = Color.white;
            _button.colors = colors;
        }
    }
    
    /// <summary>
    /// Allows a Player to select a Shop Item in the Shop
    /// </summary>
    private void SelectShopItem()
    {
        if (!string.IsNullOrEmpty(itemName))
        {
            ShopManager.Instance.selectedShopItemName.text = itemName;
            ShopManager.Instance.selectedShopItemImage.sprite = sprite;
            if (outfit != Outfit.OutfitName.Default)
            {
                ShopManager.Instance.buyAndEquipButton.SetActive(true);
                ShopManager.Instance.DisplayPreviewOutfit(outfit);
            }
            else
            {
                ShopManager.Instance.buyAndEquipButton.SetActive(false);
            }
            ShopManager.Instance.selectedShopItemImage.color = Color.white;
            ShopManager.Instance.selectedShopItemDescription.text = description;
            ShopManager.Instance.selectedShopItemPrice.text = "" + price;
            ShopManager.Instance.selectedShopItem = this;
        }
    }
}


