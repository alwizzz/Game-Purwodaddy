using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTrigger : MonoBehaviour
{
    [SerializeField] private string itemKey;
    [SerializeField] private string informationKey;
    [SerializeField] private bool isAdding;
    [SerializeField] private bool bypassStateCheck;

    public void Trigger()
    { 
        if(isAdding)
        {
            InventorySystem.Instance().AddItem(itemKey, informationKey, bypassStateCheck);
        } else
        {
            InventorySystem.Instance().RemoveItem(itemKey, informationKey, bypassStateCheck);
        }
    }
}
