using System.Collections;
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
        if(_isPlayerAlive)
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
        else
        {
            animator.SetBool("isWalk", false);
            animator.SetBool("isMeleeFight", false);
            animator.SetBool("isMeleeDead", false);
        }
    }

    private void CheckHealth()
    {
        if (_isDamageReceived)
        {
            _enemyInfo._health -= (int)_currentPlayerDamage;
            _isDamageReceived = false;
            animator.SetBool("isMeleeGotHit", true);
            if (_enemyInfo._health <= 0)
            {
                _isAlive = false;
                _allEnemies.DestroyEnemy(gameObject, 25f);
                OffCollider();
                SaveDeadEnemyPos(gameObject.transform.position);
            }
        }
        else
        {
            animator.SetBool("isMeleeGotHit", false);
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
        StartCoroutine(AttackCoroutine());
    }
    private IEnumerator AttackCoroutine()
    {
        animator.SetBool("isMeleeFight", true);
        if (_player.TryGetComponent(out Player player) && !isHitActive)
        {
            isHitActive = true;
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
            player.GiveDamage(_enemyInfo._damage);
            isHitActive = false;
        }
    }

}
