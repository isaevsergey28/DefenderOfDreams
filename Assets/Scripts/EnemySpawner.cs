using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    public Transform player;

    public EnemyMarker[] enemyMarkers;
    private IEnemyFactory _enemyFactory;

    [SerializeField] private GameObject _allEnemiesParent;
    private AllEnemies _allEnemies;

    [Inject]
    private void Construct(IEnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
        _enemyFactory.Load();
    }

    private void Start()
    {
        _allEnemies = _allEnemiesParent.GetComponent<AllEnemies>();
    }
    private void Update()
    {
        foreach(EnemyMarker marker in enemyMarkers)
        {
            if(Vector3.Distance(marker.transform.position, player.transform.position) < 100f )
            {
                if(Random.Range(0, 1000) == 10)
                {
                    _allEnemies.AddEnemy(_enemyFactory.Create(marker.enemyType, marker.transform.position));
                }
            }
        }
        
    }
    
}
 