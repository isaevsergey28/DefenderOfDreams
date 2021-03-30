using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllEnemies : MonoBehaviour
{
    [SerializeField] private List<GameObject> _allEnemies;

    public void AddEnemy(GameObject enemy)
    {
        _allEnemies.Add(enemy);
    }
    public void DestroyEnemy(GameObject enemy, float time)
    {
        _allEnemies.Remove(enemy);
        Destroy(enemy, time);
    }
}
