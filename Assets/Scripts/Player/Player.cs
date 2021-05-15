using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    enum HealingState
    {
        Healthy = 0,
        Injured = 1,
        WaitForTreatment = 2,
        Healing = 3,
    }
    [SerializeField] private float _health;
    public float damage = 15f;
    public bool isPlayerAlive { get; private set; } = true;

    public delegate void OnPlayerDead(bool isAlive);
    public static OnPlayerDead onPlayerDead;
    
    public delegate void OnPlayerHealthChange(float damage);
    public static OnPlayerHealthChange onPlayerHealthChange;
    
    private HealingState _healingState = HealingState.Healthy;
    private int _delayForHealing = 5;
    
    private float _maxHealth;
    
    
    //public delegate void OnPlayerMaxHealthChange(float damage);
    //public static OnPlayerMaxHealthChange onPlayerMaxHealthChange; // потом
    
    private void Start()
    {
        onPlayerHealthChange?.Invoke(_health);
        _maxHealth = _health;
    }

    public void GiveDamage(float damage)
    {
        _health -= damage;
        onPlayerHealthChange?.Invoke(_health);
        _healingState = HealingState.Injured;
        StopAllCoroutines();
    }

    public int GiveHealth()
    {
        return (int)_health;
    }

    private void Update()
    {
        CheckAlive();
        CheckHealingState();
        FollowToAim();
    }

    private void FollowToAim()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.one, (Input.mousePosition));
        if (groundPlane.Raycast(ray, out float hit))
        {
            transform.LookAt(ray.GetPoint(hit));
        }

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        groundPlane = new Plane(Vector3.one, -(Input.mousePosition));
        if (groundPlane.Raycast(ray, out float hit1))
        {
            transform.LookAt(ray.GetPoint(hit));
            transform.Rotate(0, 180, 0);
        }
    }

    private void CheckHealingState()
    {
        if (isPlayerAlive)
        {
            if (_healingState == HealingState.Injured)
            {
                _healingState = HealingState.WaitForTreatment;
                StartCoroutine(CheckForHeal());
            }
            if (_healingState == HealingState.Healing)
            {
                _health++;
                onPlayerHealthChange?.Invoke(_health);
                if ((int) _health >= (int) _maxHealth)
                {
                    _health = _maxHealth;
                    _healingState = HealingState.Healthy;
                }
            }
        }
    }

    private void CheckAlive()
    {
        if (_health <= 0)
        {
            isPlayerAlive = false;
            GetComponent<Animator>().SetBool("isDead", true);
            onPlayerDead.Invoke(isPlayerAlive);
        }
    }

    private IEnumerator CheckForHeal()
    {
        yield return new WaitForSeconds(5f);
        _healingState = HealingState.Healing;
    } 
}

