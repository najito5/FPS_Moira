using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OB_Target : MonoBehaviour
{
    #region Parameters
    [SerializeField, Header("item component")] INV_InventoryItemComponent ItemPack = null;
    [SerializeField, Header("Ball trigger layer")] LayerMask ballLayer = 1;
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        if (1 << other.gameObject.layer == ballLayer)
            PM_PlayerManager.Instance.PlayerOne.GetItem(ItemPack);
    }
}
