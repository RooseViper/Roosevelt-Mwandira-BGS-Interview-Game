using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Outfit
{
    public enum OutfitName{
        Default,
        Red
    }
    public OutfitName outfitName;
    public AnimatorOverrideController overrideController;

}
