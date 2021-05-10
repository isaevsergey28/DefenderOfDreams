using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour, IInventory
{
    [SerializeField] private Sprite[] _allImprovementSprites;
    public delegate void OnImprovementAdd();
    public event OnImprovementAdd onImprovementAdd;
    
    public List<Sprite> InventoryImprovements  = new List<Sprite>();

    private void Start()
    {
        Improvement.onTriggerImprovement += AddItem;
    }

    public void AddItem(GameObject improvement)
    {
        Sprite improvementSprite = null;
        
        if (improvement.TryGetComponent<Improvement>(out Improvement _improvement))
        {
            switch (_improvement.GetImprovementType())
            {
                case ImprovementType.fireRate:
                    improvementSprite = _allImprovementSprites[0];
                    break;
                case ImprovementType.damage:
                    improvementSprite = _allImprovementSprites[1];
                    break;
                case ImprovementType.speed :
                    improvementSprite = _allImprovementSprites[2];
                    break;
            }
        }
        
        InventoryImprovements.Add(improvementSprite);
        onImprovementAdd?.Invoke();
    }
}
