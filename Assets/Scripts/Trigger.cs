using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
/// <summary>
/// Checks the current trigger colliding with the Player
/// </summary>
public class Trigger : MonoBehaviour
{
    public enum TriggerType
    {
        ShopEntrance,
        ShopCounter
    }

    public TriggerType triggerType;
}
