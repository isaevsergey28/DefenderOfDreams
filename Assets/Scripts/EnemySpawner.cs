using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemySpawner : MonoBehaviour
{
    public Transform playerTransform;
    private Player player;

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
        player = playerTransform.gameObject.GetComponent<Player>();
        Debug.Log(player.isPlayerAlive);

    }
    private void Update()
    {
        foreach(EnemyMarker marker in enemyMarkers)
        {
            if(Vector3.Distance(marker.transform.position, playerTransform.transform.position) < 100f && player.isPlayerAlive)
            {
                if(Random.Range(0, 10000) == 10)
                {
                    _allEnemies.AddEnemy(_enemyFactory.Create(marker.enemyType, marker.transform.position));
                }
            }
        }
        
    }
    
}
 