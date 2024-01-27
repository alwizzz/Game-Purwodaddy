using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeInteraction : Interactable
{
    [SerializeField] private string itemKey;

    public override void Interact()
    {
        Take();
    }

    private void Take()
    {
        //InventorySystem.Instance().AddItem(itemKey);
        InformationSystem.Instance().AddInformation(itemKey);
        Destroy(gameObject);
    }
}
