
using System;
using UnityEngine;
using Zenject;
public class Improvement : MonoBehaviour
{
    public delegate void OnTriggerImprovement(GameObject improvement);
    public static event OnTriggerImprovement onTriggerImprovement;
    protected void AddToInventory()
    {
        onTriggerImprovement?.Invoke(this.gameObject);
    }
}   
