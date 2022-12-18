using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Stores the information for the Items
/// </summary>
[Serializable]
public class Item
{
    public string itemName;
    public Outfit.OutfitName outfit;
    public Sprite sprite;
    public string description;
    public float price;
    public int quantity;
}
