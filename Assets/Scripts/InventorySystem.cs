using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour, IInventory
{
    public delegate void OnImprovementAdd();
    public event OnImprovementAdd onImprovementAdd;
    
    public List<Sprite> InventoryImprovements  = new List<Sprite>();

    private void Start()
    {
        Improvement.onTriggerImprovement += AddItem;
    }

    public void AddItem(GameObject improvement)
    {
        Sprite improvementSprite = improvement.GetComponent<SpriteRenderer>().sprite;
        InventoryImprovements.Add(improvementSprite);
        onImprovementAdd?.Invoke();
    }
}
