using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Determines whether the Item is within the range of the Player.
/// </summary>
public class ItemDetector : MonoBehaviour
{
    public bool isInRange;
    public string itemInRange;
    public PickableItem pickableItem;
    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("PickableItem"))
        {
            isInRange = true;
            if (coll.GetComponent<PickableItem>() is { } pickableItem)
            {
                itemInRange = pickableItem.item;
                this.pickableItem = pickableItem;
            }
            GameManager.Instance.ShowInteractAction("Click E to Pick up");
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("PickableItem"))
        {
            isInRange = false;
            itemInRange = "";
            pickableItem = null;
            GameManager.Instance.interactCanvas.enabled = false;
        }
    }
}
