using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    /// <summary>
    /// Scriptable object storing all the Items in the shop
    /// </summary>
    public ShopInventory shopInventory;
    public GameObject shopItemsParent;
    [HideInInspector]
    public List<ShopItem> shopItems;
    // Start is called before the first frame update
    void Start()
    {
        shopItems = shopItemsParent.GetComponentsInChildren<ShopItem>().ToList();
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
    }
}
