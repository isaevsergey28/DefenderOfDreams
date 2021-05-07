using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;
    public float damage = 15f;
    public bool isPlayerAlive { get; private set; } = true;

    public delegate void OnPlayerDead(bool isAlive);
    public static OnPlayerDead onPlayerDead;
    
    public delegate void OnPlayerHealthChange(float damage);
    public static OnPlayerHealthChange onPlayerHealthChange;

    //public delegate void OnPlayerMaxHealthChange(float damage);
    //public static OnPlayerMaxHealthChange onPlayerMaxHealthChange; // потом
    
    private void Start()
    {
        onPlayerHealthChange?.Invoke(_health);
    }

    public void GiveDamage(float damage)
    {
        _health -= damage;
        onPlayerHealthChange?.Invoke(_health);
    }
    private void Update()
    {
        if(_health <= 0)
        {
            isPlayerAlive = false;
            onPlayerDead.Invoke(isPlayerAlive);
        }
    }
}
