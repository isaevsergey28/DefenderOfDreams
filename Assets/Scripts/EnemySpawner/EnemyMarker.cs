using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMarker : MonoBehaviour
{
    public EnemyType enemyType;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 0.5f);
        Gizmos.color = Color.white;
    }

}

public enum EnemyType
{
    Melee = 0,
    Ranged = 1,
    Explosive = 2,
}