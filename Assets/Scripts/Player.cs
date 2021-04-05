using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 10;
    public float damage = 15f;
    private bool _isPlayerAlive = true;

    public delegate void OnPlayerDead();
    static public OnPlayerDead onPlayerDead;

    public void GiveDamage(float damage)
    {
        _health -= damage;
    }

    private void Update()
    {
        if(_health <= 0)
        {
            _isPlayerAlive = false;
            onPlayerDead.Invoke();
        }
    }
}
