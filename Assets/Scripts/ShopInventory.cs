using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New ScriptableObject", menuName = "ScriptableObjects/ShopInventory", order = 1)]
public class ShopInventory : ScriptableObject
{
    /// <summary>
    /// The Items for sale in the Shop.
    /// </summary>
    public Item[] items;
    /// <summary>
    /// The balance of the Shop keeper.
    /// </summary>
    public float balance;
  
}
