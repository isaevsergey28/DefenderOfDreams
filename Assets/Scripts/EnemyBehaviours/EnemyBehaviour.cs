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

    public float _animDeathTime { get; set; }

    [Inject]
    private void Construct(AllEnemies allEnemies)
    {
        _allEnemies = allEnemies;
    }
    private void Awake()
    {
        animator = GetComponent<Animator>();
        _player = GameObject.Find("Player").gameObject;
    }
    
    public virtual void Attack() {}

    public void Destroy(GameObject enemy, float time)
    {
        _allEnemies.DestroyEnemy(enemy, time);
    }

    public EnemyType GetEnemyType()
    {
        return _enemyType;
    }
    
    public void KillEnemy()
    {
        _isAlive = false;
    }
    public void GiveDamage(float damage)
    {
        _currentPlayerDamage = damage;
        _isDamageReceived = true;
    }
}
