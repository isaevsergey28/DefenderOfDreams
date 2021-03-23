using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class EnemyFactory : IEnemyFactory
{
    private object _meleeEnemyPrefab;
    private object _rangedEnemyPrefab;
    private object _explosiveEnemyPrefab;

    private DiContainer _diContainer;

    private GameObject _enemiesParent;

    public EnemyFactory(DiContainer diContainer)
    {
        _diContainer = diContainer;
        _enemiesParent = GameObject.Find("AllEnemies");
    }

    public void Load()
    {
        _meleeEnemyPrefab = Resources.Load("MeleeEnemy");
        _rangedEnemyPrefab = Resources.Load("RangedEnemy");
        _explosiveEnemyPrefab = Resources.Load("ExplosiveEnemy");
    }
    public GameObject Create(EnemyType enemyType, Vector3 at)
    {
        GameObject enemy = null;
        switch(enemyType)
        {
            case EnemyType.Melee:
                enemy = _diContainer.InstantiatePrefab((GameObject)_meleeEnemyPrefab, at, Quaternion.identity, _enemiesParent.transform);
                break;
            case EnemyType.Ranged:
                enemy = _diContainer.InstantiatePrefab((GameObject)_rangedEnemyPrefab, at, Quaternion.identity, _enemiesParent.transform);
                break;
            case EnemyType.Explosive:
                enemy = _diContainer.InstantiatePrefab((GameObject)_explosiveEnemyPrefab, at, Quaternion.identity, _enemiesParent.transform);
                break;
        }
        return enemy;
    }

}
