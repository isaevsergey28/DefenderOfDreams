using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBehaviour : EnemyBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    private bool _isWantToExplode = false;
    private ParticleSystem _explosionAnim;
    private MeshRenderer _enemyBody;
    [SerializeField] private float _damageRadius = 2f;

    private void Start()
    {
        _enemyBehaviour = GetComponent<Arrive>();
        _explosionAnim = _explosionPrefab.GetComponent<ParticleSystem>();
        _enemyBody = GetComponent<MeshRenderer>();
        _enemyInfo = GetComponent<EnemyInfo>();
    }

    private void Update()
    {
        if(_isPlayerAlive)
        {
            transform.LookAt(_player.transform);
            CheckForAttack();
            CheckHealth();
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
                StartCoroutine(Explode(0f));
            }
        }
    }

    private void CheckForAttack()
    {
        if (_enemyBehaviour.isTimeToAttack && !_isWantToExplode)
        {
            _isWantToExplode = true;
            Attack();
        }
    }

    public override void Attack()
    {
        if(_isAlive)
        {
            StartCoroutine(Explode(1f));
        }
    }
    
    public IEnumerator Explode(float timeToExplode)
    {
        yield return new WaitForSeconds(timeToExplode);
        _attackAudio.Play();
        HideEnemy();
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        DestroyEnemyAndExplosion(explosion);
        SaveDeadEnemyPos(gameObject.transform.position);

        DamageNearbyAgents();
    }

    private void DamageNearbyAgents()
    {
        Vector3 distance = transform.position - _player.transform.position;
        if (distance.magnitude <= _damageRadius)
        {
            if (_player.TryGetComponent<Player>(out Player player))
            {
                player.GiveDamage(_enemyInfo._damage / distance.magnitude);
            }
        }

        if (transform.root.TryGetComponent<AllEnemies>(out AllEnemies allEnemies))
        {
            foreach (var enemy in allEnemies.GetEnemies())
            {
                GameObject currentEnemy = enemy.gameObject;
                distance = transform.position - currentEnemy.transform.position;
                if (distance.magnitude <= _damageRadius)
                {
                    if(currentEnemy.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemyBehaviour))
                    {
                        enemyBehaviour.GiveDamage(_enemyInfo._damage / distance.magnitude);
                    }
                }
            }
        }
    }

    private void DestroyEnemyAndExplosion(GameObject explosion)
    {
        base.Destroy(gameObject, _explosionAnim.main.duration);
        Destroy(explosion, _explosionAnim.main.duration);
    }

    private void HideEnemy()
    {
        _enemyBody.enabled = false;
        if (_enemyBody.TryGetComponent<MeshRenderer>(out MeshRenderer meshRenderer))
        {
            meshRenderer.enabled = false;
            DisableChilds();
        }
    }

    public void DisableChilds() 
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
