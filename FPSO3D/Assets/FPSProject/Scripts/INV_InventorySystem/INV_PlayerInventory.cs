using System;
using System.Collections.Generic;
using UnityEngine;
public class INV_PlayerInventory : MonoBehaviour
{
    Dictionary<int, INV_InventoryItem> playerInventory = new Dictionary<int, INV_InventoryItem>();
    
    public static event Action<Dictionary<int, INV_InventoryItem>> OnRefreshInventory = null;
    public bool CheckID(int _id) => playerInventory.ContainsKey(_id);
    public INV_InventoryItem GetFromID(int _id) => playerInventory[_id];

    public void AddItem(INV_InventoryItemComponent _itemToAdd)
    {
        INV_InventoryItem _item = INV_InventoryManager.Instance.GetItem(_itemToAdd.Item.ID);
        AddInventory(_item);
    }

    public void AddInventory(INV_InventoryItem _item)
    {
        if (!_item) return;
        _item.OnUseItem = UseItemInv;
        if (!CheckID(_item.ID))
            playerInventory.Add(_item.ID, _item);
        else
            GetFromID(_item.ID).AddStack();
        OnRefreshInventory?.Invoke(playerInventory);
    }

    public void UseItemInv(INV_InventoryItem _item)
    {
        if (!CheckID(_item.ID)) return;
        GetFromID(_item.ID).RemoveStack();
        if (_item.Quantity <= 0) playerInventory.Remove(_item.ID);
        OnRefreshInventory?.Invoke(playerInventory);
    }
}
