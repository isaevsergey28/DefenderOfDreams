using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndMapSystem : MonoBehaviour
{
    [SerializeField] private GameObject _newPlayerPos;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.gameObject.transform.position = _newPlayerPos.transform.position;
            player.GiveDamage(player.GiveHealth() * 0.2f);
        }
        else if (other.gameObject.TryGetComponent<EnemyBehaviour>(out EnemyBehaviour enemyBehaviour))
        {
            enemyBehaviour.Destroy(other.gameObject, 0f);
        }
    }
}
