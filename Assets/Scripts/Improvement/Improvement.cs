
using System;
using UnityEngine;
using Zenject;
public class Improvement : MonoBehaviour
{
   
    [SerializeField] protected ImprovementType improvementType;
    public delegate void OnTriggerImprovement(GameObject improvement);
    public static event OnTriggerImprovement onTriggerImprovement;
    protected void AddToInventory()
    {
        onTriggerImprovement?.Invoke(this.gameObject);
    }

    public ImprovementType GetImprovementType()
    {
        return improvementType;
    }
}   
public enum ImprovementType
{
    damage = 0,
    fireRate = 1,
    speed = 2,
}

