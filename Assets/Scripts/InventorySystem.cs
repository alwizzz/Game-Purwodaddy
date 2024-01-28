using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : StaticReference<InventorySystem>
{
    [SerializeField] private List<string> inventory = new List<string>();

    private void Awake()
    {
        BaseAwake(this);
    }

    public void AddItem(string itemKey, string informationKey)
    {
        if (HasItem(itemKey)) 
        { 
            print($"INVENTORY: duplication warning! failed ({itemKey})");
            return;
        }

        inventory.Add(itemKey);
        print($"INVENTORY: {itemKey} added to inventory");
        InventoryUI.Instance().UpdateShownItems(inventory);

        InformationSystem.Instance().AddInformation(informationKey);
    }

    public bool HasItem(string itemKey)
    {
        return inventory.Contains(itemKey);
    }

    public void RemoveItem(string itemKey, string informationKey)
    {
        if (!HasItem(itemKey))
        {
            print($"INVENTORY: not found ({itemKey})");
            return;
        }

        inventory.Remove(itemKey);
        print($"INVENTORY: {itemKey} removed from inventory");
        InventoryUI.Instance().UpdateShownItems(inventory);

        InformationSystem.Instance().AddInformation(informationKey);
    }




    private void OnDestroy()
    {
        BaseOnDestroy();
    }
}
