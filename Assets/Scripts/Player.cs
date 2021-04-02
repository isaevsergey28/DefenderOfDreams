using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health = 100;
    public float damage = 15f;

    public void GiveDamage(float damage)
    {
        _health -= damage;
    }
}
