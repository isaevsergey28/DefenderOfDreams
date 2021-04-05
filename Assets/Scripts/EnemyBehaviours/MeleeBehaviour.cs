﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour : EnemyBehaviour
{
    public bool isHitActive { get; private set; } = false;

    private void Start()
    {
        _enemyBehaviour = GetComponent<Arrive>();
        _enemyInfo = GetComponent<EnemyInfo>();
        animator.SetBool("isWalk", true);
    }
    private void Update()
    {
        if (_isAlive)
        {
            CheckForAttack();
            CheckHealth();
        }
        else
        {
            EnableDeadAnim();
            _animDeathTime = animator.GetCurrentAnimatorStateInfo(0).length;

        }
    }

    private void CheckHealth()
    {
        if (_isDamageReceived)
        {
            _enemyInfo._health -= _currentPlayerDamage;
            _isDamageReceived = false;
            if (_enemyInfo._health <= 0)
            {
                _isAlive = false;
                _allEnemies.DestroyEnemy(gameObject, 1f);
                OffCollider();
                SaveDeadEnemyPos(gameObject.transform.position);
            }
        }
    }

    private void EnableDeadAnim()
    {
        _enemyBehaviour.isStopped = true;
        animator.SetBool("isWalk", false);
        animator.SetBool("isMeleeFight", false);
        animator.SetBool("isMeleeDead", true);
    }

    private void LateUpdate()
    {
        if(_isAlive)
        {
            transform.LookAt(_player.transform);
        }
    }
    private void CheckForAttack()
    {
        if (_enemyBehaviour.isTimeToAttack)
        {
            animator.SetBool("isWalk", false);
            _enemyBehaviour.isStopped = true;
            Attack();
            _enemyBehaviour.CheckDistance();
        }
        else
        {
            isHitActive = false;
            _enemyBehaviour.isStopped = false;
            animator.SetBool("isWalk", true);
            animator.SetBool("isMeleeFight", false);
        }
    }
    public override void Attack()
    {
        isHitActive = true;
        animator.SetBool("isMeleeFight", true);
    }
}