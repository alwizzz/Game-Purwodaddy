using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : StaticReference<InventoryUI>
{
    [SerializeField] private GameObject itemKunci;
    [SerializeField] private GameObject itemKeranjang;
    [SerializeField] private GameObject itemNasiBungkus;

    private void Awake()
    {
        BaseAwake(this);

        itemKeranjang.SetActive(false);
        itemKunci.SetActive(false);
        itemNasiBungkus.SetActive(false);
    }

    public void UpdateShownItems(List<string> inventory)
    {
        itemKunci.SetActive(
            (inventory.Contains("item-kunci") ? true : false)
        );
        itemKeranjang.SetActive(
            (inventory.Contains("item-keranjang") ? true : false)
        );
        itemNasiBungkus.SetActive(
            (inventory.Contains("item-nasibungkus") ? true : false)
        );
    }


    private void OnDestroy()
    {
        BaseOnDestroy();
    }

}
