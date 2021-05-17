using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBehaviour : EnemyBehaviour
{
    private bool isReadyToAttack = true;
    public bool isAtacking { get; set; } = false;

    private float _animationReloadTime = 0.25f;

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
            isAtacking = false;
            animator.SetBool("isWalk", false);
            animator.SetBool("isRangedFight", false);
            animator.SetBool("isRangedDead", false);
        }
        
    }

    private void CheckHealth()
    {
        if(_isDamageReceived)
        {
            _enemyInfo._health -= (int)_currentPlayerDamage;
            _isDamageReceived = false;
            if(_enemyInfo._health <= 0)
            {
                _isAlive = false;
                Destroy(gameObject, 25f);
                GetComponent<Rigidbody>().useGravity = true;
                //OffCollider();
                SaveDeadEnemyPos(gameObject.transform.position);
            }
        }
    }
    private void EnableDeadAnim()
    {
        _enemyBehaviour.isStopped = true;
        animator.SetBool("isWalk", false);
        animator.SetBool("isRangedFight", false);
        animator.SetBool("isRangedDead", true);
    }
    private void LateUpdate()
    {
        if (_isAlive)
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
            if(isReadyToAttack)
            {
                Attack();
                StartCoroutine(ReloadBow());
            }
            _enemyBehaviour.CheckDistance();
        }
        else
        {
            isAtacking = false;
            _enemyBehaviour.isStopped = false;
            animator.SetBool("isRangedFight", false);
            animator.SetBool("isWalk", true);
        }
    }
    public override void Attack()
    {
        animator.SetBool("isRangedFight", true);
        StartCoroutine(AttackDelay());
        isReadyToAttack = false;
    }
    private IEnumerator ReloadBow()
    {
        yield return new WaitForSeconds(_animationReloadTime);
        isReadyToAttack = true;
    }
    private IEnumerator AttackDelay()
    {
        yield return new WaitForSeconds(_animationReloadTime);
        isAtacking = true;
    }
}
