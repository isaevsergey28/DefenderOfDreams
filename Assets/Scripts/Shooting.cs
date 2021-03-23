using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private Transform _mainCamera;
    [SerializeField] private AudioSource _shotAudio;

    [SerializeField] private GameObject _allEnemiesParent;
    private AllEnemies _allEnemies;

    [SerializeField] private GameObject _explosionPrefab;
    [SerializeField] private Transform _spawnExplosionPos;
    private ParticleSystem _explosionAnim;

    private void Start()
    {
        _allEnemies = _allEnemiesParent.GetComponent<AllEnemies>();
        _explosionAnim = _explosionPrefab.GetComponent<ParticleSystem>();
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _shotAudio.Play();
            GameObject explosion;

            Vector3 direction = _mainCamera.TransformDirection(Vector3.forward);
            explosion = Instantiate(_explosionPrefab, _spawnExplosionPos.position, Quaternion.identity, _spawnExplosionPos);
            DestroyEsplosion(explosion);
            if (Physics.Raycast(_mainCamera.position, direction, out RaycastHit hit, Mathf.Infinity))
            {
                if(hit.collider.gameObject.TryGetComponent(out Arrive enemy))
                {
                    _allEnemies.DestroyEnemy(enemy.gameObject);
                }
            }

        }
    }
    private void DestroyEsplosion(GameObject explosion)
    {
        Destroy(explosion, _explosionAnim.main.duration);
    }
}
