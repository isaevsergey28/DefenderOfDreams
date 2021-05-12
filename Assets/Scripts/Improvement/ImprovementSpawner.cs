using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImprovementSpawner : MonoBehaviour
{
   [SerializeField] private GameObject[] _allImprovements;

   private void Start()
   {
        EnemyBehaviour.onEnemyDead += SpawnImprovement;
   }

    private void SpawnImprovement(Vector3 enemyDeadPosition)
    {
        enemyDeadPosition = new Vector3(enemyDeadPosition.x, enemyDeadPosition.y, enemyDeadPosition.z + 3f);
        Instantiate(_allImprovements[Random.Range(0, _allImprovements.Length)], enemyDeadPosition, Quaternion.identity, transform);
    }
}
