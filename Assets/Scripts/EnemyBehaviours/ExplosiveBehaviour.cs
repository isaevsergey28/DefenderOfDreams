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
    }

    private void Update()
    {
        transform.LookAt(GameObject.Find("Player").gameObject.transform);
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
        _enemyBody.enabled = false;
        _enemyBody.GetComponentInChildren<MeshRenderer>().enabled = false;
        GameObject explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        base.Destroy(gameObject, _explosionAnim.main.duration);
        Destroy(explosion, _explosionAnim.main.duration);   
    }
}
