using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;
using System;

public class INV_ItemUI : MonoBehaviour
{
    #region Parameters
    [SerializeField, Header("Icon")] Image icon = null;
    [SerializeField, Header("Quantity Text")] TMP_Text textQuantity = null;
    INV_InventoryItem itemStored = null;
    #endregion

    #region Getter
    public string Name => itemStored ? itemStored.name : "";
    public int Quantity => itemStored ? itemStored.Quantity : 0;
    public bool IsValid => icon && textQuantity;
    #endregion

    public void SetData(INV_InventoryItem _item)
    {
        if (!IsValid) return;
        if (!_item) return;
        itemStored = _item;
        icon.sprite = _item.Icon;
        textQuantity.text = $"x{Quantity}";
    }
}
