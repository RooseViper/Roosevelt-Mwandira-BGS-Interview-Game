using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages and lets a Player switch outfits. This is attached to the Player character and additonally if we had different Playable characters, this can be used for
/// each character according to their outfits.
/// </summary>
public class OutFitManager : MonoBehaviour
{
    /// <summary>
    /// Stores a collection of different outfits.
    /// </summary>
    public Outfit[] outfits;

    private PlayerController _playerController;

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
    }

    public void ChangeOutfit(Outfit.OutfitName outfitName)
    {
        //Looks up an outfit based on the name.
        Outfit outfit = Array.Find(outfits, fit => fit.outfitName == outfitName);
        //Overrides the current outfit in the Animator controller
        _playerController.Animator.runtimeAnimatorController = outfit.overrideController;
    }
}
