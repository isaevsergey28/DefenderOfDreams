using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour, IEnemyBehaviour
{
    [SerializeField] protected int _hp;
    protected Arrive _enemyBehaviour;
    [SerializeField] protected AudioSource _attackAudio;

    public virtual void Attack()
    {
        
    }
}
