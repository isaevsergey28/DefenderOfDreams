using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBehaviour : EnemyBehaviour
{
    [SerializeField] private GameObject _explosionPrefab;
    private bool _isWantToExplode = false;
    private ParticleSystem _explosionAnim;

    private void Start()
    {
        _enemyBehaviour = GetComponent<Arrive>();
        _explosionAnim = _explosionPrefab.GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        if(_enemyBehaviour.isTimeToAttack && !_isWantToExplode)
        {
            _enemyBehaviour.isStopped = true;
            _isWantToExplode = true;
            Attack();
        }
    }
    public override void Attack()
    {
        StartCoroutine(Explode());
    }
    
    private IEnumerator Explode()
    {
        
        yield return new WaitForSeconds(1f);
        _attackAudio.Play();
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        Destroy(explosion, _explosionAnim.main.duration); // уничтожить врага из списка 
    }
}
