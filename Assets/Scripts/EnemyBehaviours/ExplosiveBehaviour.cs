using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBehaviour : EnemyBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    private bool _isWantToExplode = false;
    private ParticleSystem _explosionAnim;
    private MeshRenderer _enemyBody;

    private void Start()
    {
        _enemyBehaviour = GetComponent<Arrive>();
        _explosionAnim = _explosionPrefab.GetComponent<ParticleSystem>();
        _enemyBody = GetComponent<MeshRenderer>();
        _enemyInfo = GetComponent<EnemyInfo>();
    }

    private void Update()
    {
        transform.LookAt(_player.transform);
        CheckForAttack();
        CheckHealth();
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
    }

    private void DestroyEnemyAndExplosion(GameObject explosion)
    {
        base.Destroy(gameObject, _explosionAnim.main.duration);
        Destroy(explosion, _explosionAnim.main.duration);
    }

    private void HideEnemy()
    {
        _enemyBody.enabled = false;
        _enemyBody.GetComponent<MeshRenderer>().enabled = false;
        DisableChilds();
    }

    public void DisableChilds() 
    {
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
