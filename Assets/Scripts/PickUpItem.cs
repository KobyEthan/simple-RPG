using UnityEngine;
using System.Collections;

public class PickUpItem : Interactable
{
    public Item ItemDrop { get; set; }
    public override void Interact()
    {
        InventoryController.Instance.GiveItem(ItemDrop);
        Destroy(gameObject);
    }
}