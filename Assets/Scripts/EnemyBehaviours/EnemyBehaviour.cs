using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyBehaviour : MonoBehaviour, IEnemyBehaviour
{
    protected EnemyInfo _enemyInfo;
    protected Arrive _enemyBehaviour;
    [SerializeField] protected AudioSource _attackAudio;
    protected AllEnemies _allEnemies;

    [SerializeField] protected EnemyType _enemyType;
    protected GameObject _player;

    protected Animator animator;

    protected bool _isAlive = true;
    protected bool _isDamageReceived = false;
    protected float _currentPlayerDamage;

    public delegate void OnEnemyDead(Vector3 position);
    static public event OnEnemyDead onEnemyDead;

    public float _animDeathTime { get; set; }

    private BoxCollider _boxCollider;

    protected bool _isPlayerAlive = true;

    [Inject]
    private void Construct(AllEnemies allEnemies)
    {
        _allEnemies = allEnemies;

    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        _player = GameObject.Find("Player").gameObject;
        _boxCollider = GetComponent<BoxCollider>();
        Player.onPlayerDead += StopEnemy;
    }

    public virtual void Attack() { }

    public void Destroy(GameObject enemy, float time)
    {
        _allEnemies.DestroyEnemy(enemy, time);
    }

    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag.Equals("Ground") && GetEnemyType() == EnemyType.Ranged && _enemyInfo._health <=0)
        {
            OffCollider();
            gameObject.transform.Translate(Vector3.up);
        }
    }

    protected void OffCollider()
    {
        _boxCollider.enabled = false;
        GetComponent<Rigidbody>().useGravity = false;
    }
    public void GiveDamage(float damage)
    {
        _currentPlayerDamage = damage;
        _isDamageReceived = true;
    }

    public void SaveDeadEnemyPos(Vector3 enemyPosition)
    {
        onEnemyDead.Invoke(enemyPosition);
    }

    private void StopEnemy(bool isAlive)
    {
        _isPlayerAlive = isAlive;
        _enemyBehaviour.isStopped = true;
    }
}
