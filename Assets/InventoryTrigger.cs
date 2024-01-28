using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTrigger : MonoBehaviour
{
    [SerializeField] private string itemKey;
    [SerializeField] private string informationKey;
    [SerializeField] private bool isAdding;

    public void Trigger()
    { 
        if(isAdding)
        {
            InventorySystem.Instance().AddItem(itemKey, informationKey);
        } else
        {
            InventorySystem.Instance().RemoveItem(itemKey, informationKey);
        }
    }
}
