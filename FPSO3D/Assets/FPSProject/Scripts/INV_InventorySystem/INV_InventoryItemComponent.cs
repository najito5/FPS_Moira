using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INV_InventoryItemComponent : MonoBehaviour
{
    [SerializeField, Header("Data about item")] INV_InventoryItem item = null;
    public INV_InventoryItem Item => item;
    public bool IsValid => item;

    private void Start()=>Init();
    void Init()
    {
        if (!IsValid) return;
        //name = item.name;
    }
}
