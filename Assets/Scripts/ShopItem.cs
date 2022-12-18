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
    
    public void Initialize()
    {
        //Checks if an Item has been loaded
        if (!string.IsNullOrEmpty(itemName))
        {
            image.color = Color.white;
            image.sprite = sprite;
            quantityText.text = "" + quantity;
            priceText.text = "" + price;
        }
        else
        {
            image.color = Color.clear;
            quantityText.text = "";
            priceText.text = "";
        }
    }
}


