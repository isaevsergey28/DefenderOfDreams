using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyBehaviour : MonoBehaviour, IEnemyBehaviour
{
    [SerializeField] protected int _hp;
    protected Arrive _enemyBehaviour;
    [SerializeField] protected AudioSource _attackAudio;
    private AllEnemies _allEnemies;

    [SerializeField] protected EnemyType _enemyType;
    [SerializeField] protected GameObject _player;

    protected Animation _deathAnim;

    [Inject]
    private void Construct(AllEnemies allEnemies)
    {
        _allEnemies = allEnemies;
    }
    private void Awake()
    {
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

    public float GetAnimTime()
    {
        return _deathAnim.clip.length;
    }
}
