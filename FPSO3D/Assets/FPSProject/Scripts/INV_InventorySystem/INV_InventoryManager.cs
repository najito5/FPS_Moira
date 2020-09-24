using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INV_InventoryManager : Singleton<INV_InventoryManager>
{
    #region Parameters
    [SerializeField, Header("Resources folder name")] string folderBase = "items";
    [SerializeField, Header("Resources folder pack name")] string packFolderName = "pack";
    [SerializeField, Header("Base item UI Object")] INV_ItemUI itemPrefab = null;
    [SerializeField, Header("Anchor")] RectTransform anchor = null;
    [SerializeField, Header("Inventory UI Group")] GameObject inventoryGroup = null;
    #endregion
    Dictionary<int, INV_InventoryItem> dataItemPack = new Dictionary<int, INV_InventoryItem>();

    public bool IsValid => inventoryGroup;
    public INV_InventoryItem GetItem(int _id) => dataItemPack[_id];

    void Start()
    {
        GetDataBase();
        INV_PlayerInventory.OnRefreshInventory += RefreshUIInventory;
    }

    void GetDataBase() => dataItemPack = LoadDataBase($"{folderBase}/{packFolderName}");

    Dictionary<int, INV_InventoryItem> LoadDataBase(string _resource)
    {
        INV_InventoryItem[] _items = Resources.LoadAll<INV_InventoryItem>(_resource);
        Dictionary<int, INV_InventoryItem> _dataBase = new Dictionary<int, INV_InventoryItem>();
        int _totalData = _items.Length;
        for (int i = 0; i < _totalData; i++)
        {
            _dataBase.Add(_items[i].ID, _items[i]);
        }
        return _dataBase;
    }

    void RefreshUIInventory(Dictionary<int, INV_InventoryItem> _data)
    {
        if (!IsValid) return;
        ClearUI(anchor);
        foreach (KeyValuePair<int, INV_InventoryItem> _item in _data)
        {
            INV_ItemUI _i = Instantiate(itemPrefab, anchor);
            _i.SetData(_item.Value);
        }
    }

    void ClearUI(Transform _tr)
    {
        if (!_tr) return;
        for (int i = 0; i < _tr.childCount; i++)
        {
            Destroy(_tr.GetChild(i).gameObject);
        }
    }
}
