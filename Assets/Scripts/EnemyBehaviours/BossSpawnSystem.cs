using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawnSystem : MonoBehaviour
{
    [SerializeField] private GameObject _boss;
    
    private Vector3 _spawnPosition;
    public void SpawnBoss()
    {
        RandomizeSpawnPos();
        Instantiate(_boss, _spawnPosition, Quaternion.identity, null);
    }

    private void RandomizeSpawnPos()
    {
        _spawnPosition = transform.position + new Vector3(Random.Range(-50, 50), 0, Random.Range(-30, 30));
    }
}
