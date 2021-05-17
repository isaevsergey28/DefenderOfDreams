using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FireballBehaviour : MonoBehaviour
{
    private Vector3 _playerPos;
    [SerializeField] private float _arrowSpeed = 45f;
    private int _damage;
    private void Start()
    {
        _playerPos = GameObject.Find("Player").transform.position;
        if(GameObject.Find("RangedEnemy(Clone)").TryGetComponent<EnemyInfo>(out EnemyInfo enemyInfo))
        {
            _damage = enemyInfo._damage;
        }
        else
        {
            _damage = 10;
        }
       
    }
   private void Update()
   {
        transform.position =  Vector3.MoveTowards(transform.position, _playerPos, Time.deltaTime * _arrowSpeed);
   }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.GiveDamage(_damage);
        }
    }
}
 