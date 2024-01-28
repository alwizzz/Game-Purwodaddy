using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeInteraction : Interactable
{
    [SerializeField] private string itemKey;
    [SerializeField] private string informationKey;


    public override void Interact()
    {
        Take();
    }

    private void Take()
    {
        InventorySystem.Instance().AddItem(itemKey, informationKey);
        //InformationSystem.Instance().AddInformation(itemKey);
        Destroy(gameObject);
    }
}
