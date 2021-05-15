using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InventoryWindow : MonoBehaviour
{
    [SerializeField] private Font _iconTextFont;
    
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
                var icon = CreateIcon(improvement);
                _drawnIcons.Add(icon);
            }
        }
    }

    private GameObject CreateIcon(Sprite improvement)
    {
        GameObject icon = new GameObject("Icon");
        icon.transform.parent = this.gameObject.transform;
        icon.AddComponent<CanvasRenderer>();
        icon.AddComponent<Image>().sprite = improvement;
        GameObject text = new GameObject("Text");
        text.transform.SetParent(icon.transform, false);
        text.transform.position = icon.transform.position;
        text.AddComponent<Text>().text = "1";
        text.GetComponent<Text>().fontSize = 5;
        text.GetComponent<Text>().resizeTextForBestFit = true;
        text.GetComponent<Text>().font = _iconTextFont;
        text.GetComponent<Text>().color = Color.black;
        return icon;
    }

    private bool CheckIconExistInInventory(Sprite improvement)
    {
        foreach (var icon in _drawnIcons)
        {
            if (icon.GetComponent<Image>().sprite.name == improvement.name)
            {
                CheckImprovementCount(icon);
                return true;
            }
        }
        return false;
    }

    private static void CheckImprovementCount(GameObject icon)
    {
        int improvementCount = Int32.Parse(icon.transform.GetChild(0).GetComponent<Text>().text);
        improvementCount++;
        icon.transform.GetChild(0).GetComponent<Text>().text = improvementCount.ToString();
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
