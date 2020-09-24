using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public abstract class INV_InventoryItem : ScriptableObject
{
    #region Parameters
    [SerializeField] int id = 0;
    [SerializeField] string nameItem = "item";
    [SerializeField] Sprite icon = null;
    [NonSerialized] int quantity = 1;
    #endregion

    #region Getter
    public int ID => id;
    public string Name => nameItem;
    public Sprite Icon => icon;
    public int Quantity => quantity;
    #endregion

    #region Action
    public Action<INV_InventoryItem> OnUseItem = null;
    #endregion

    public virtual void Use() => OnUseItem?.Invoke(this);
    public void AddStack() => quantity++;
    public void RemoveStack() => quantity--;
}
