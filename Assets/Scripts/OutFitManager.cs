using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages and lets a Player use an Outfit. This is attached to the Player character and additonally if we had different Playable characters, this can be used for
/// each character according to their outfits.
/// </summary>
public class OutFitManager : MonoBehaviour
{
    /// <summary>
    /// Stores a collection of different outfits.
    /// </summary>
    public Outfit[] outfits;

    public void ChangeOutfit(Outfit.OutfitName outfitName)
    {
        //Looks up an outfit based on the name.
        Outfit outfit = Array.Find(outfits, fit => fit.outfitName == outfitName);
        //Overrides the current outfit in the Animator controller
        InventoryManager.Instance.playerController.Animator.runtimeAnimatorController = outfit.overrideController;
    }
}
