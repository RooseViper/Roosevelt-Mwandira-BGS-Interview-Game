using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Stores Information for Items in the Players Inventory
/// </summary>
public class InventoryItem : MonoBehaviour
{
    public string itemName;
    public Outfit.OutfitName outfit;
    public bool isOutfit = false;
    public Image image;
    public Text quantityText;
    private  Button _button;
    private void Awake()
    {
        image = transform.GetChild(0).GetComponent<Image>();
        quantityText = GetComponentInChildren<Text>();
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SelectIInventoryItem);
    }
    /// <summary>
    /// Selects Item in the Inventory
    /// </summary>
    private void SelectIInventoryItem()
    {
        if (isOutfit)
        {
            InventoryManager.Instance.outFitManager.ChangeOutfit(outfit);
            
        }
        AudioController.Instance.Play("Click");
    }

    public void Clear()
    {
        itemName = "";
        image.enabled = false;
        quantityText.text = "";
    }
}
