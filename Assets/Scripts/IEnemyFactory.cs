using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyFactory
{
    void Load();
    GameObject Create(EnemyType enemyType, Vector3 at);
}
