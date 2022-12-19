using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Switches the preview information of an Item from both the Buying and Selling section of the Shop.
/// </summary>
public class ShopSectionDisplaySelector : MonoBehaviour
{
    public Text selectedShopItemNameBuying;
    public Image selectedShopItemImageBuying;
    public Text selectedShopItemDescriptionBuying;
    public Text selectedShopItemPriceBuying;
    
    public Text selectedShopItemNameSelling;
    public Image selectedShopItemImageSelling;
    public Text selectedShopItemDescriptionSelling;
    public Text selectedShopItemPriceSelling;

    public void SwitchPreviewInformation(bool isBuying)
    {
        if (isBuying)
        {
            ShopManager.Instance.selectedShopItemName = selectedShopItemNameBuying;
            ShopManager.Instance.selectedShopItemImage = selectedShopItemImageBuying;
            ShopManager.Instance.selectedShopItemDescription = selectedShopItemDescriptionBuying;
            ShopManager.Instance.selectedShopItemPrice = selectedShopItemPriceBuying;
        }
        else
        {
             ShopManager.Instance.selectedShopItemName =  selectedShopItemNameSelling ;
             ShopManager.Instance.selectedShopItemImage =  selectedShopItemImageSelling ;
             ShopManager.Instance.selectedShopItemDescription =  selectedShopItemDescriptionSelling ;
             ShopManager.Instance.selectedShopItemPrice =  selectedShopItemPriceSelling ;
        }
    }

}
