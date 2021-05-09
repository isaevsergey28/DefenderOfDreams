using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryWindow : MonoBehaviour
{
    private InventorySystem _inventorySystem;
    private List<GameObject> _drawnIcons = new List<GameObject>();
    private void Start()
    {
        _inventorySystem = GetComponent<InventorySystem>();
        _inventorySystem.onImprovementAdd += RedrawInventory;
    }
    private void RedrawInventory()
    {
        ClearDrawnIcons();
        foreach (var improvement in _inventorySystem.InventoryImprovements)
        {
            if (!CheckIconExistInInventory(improvement))
            {
                GameObject icon;
                icon = new GameObject("Icon");
                icon.transform.parent = this.gameObject.transform;
                icon.AddComponent<Image>().sprite = improvement;
                _drawnIcons.Add(icon);  
            }
        }
    }
    private bool CheckIconExistInInventory(Sprite improvement)
    {
        foreach (var icon in _drawnIcons)
        {
            if (icon.GetComponent<Image>().sprite.name == improvement.name)
            {
                return true;
            }
        }
        return false;
    }
    private void ClearDrawnIcons()
    {
        foreach (var icon in _drawnIcons)
        {
            Destroy(icon);
        }
        _drawnIcons.Clear();
    }
}
