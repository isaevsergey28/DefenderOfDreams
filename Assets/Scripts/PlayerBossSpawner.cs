using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _callBossPanel;
    private bool _bossIsSpawned = false;

    private void OnTriggerEnter(Collider other)
    {
        if (_bossIsSpawned == false)
        {
            if (other.TryGetComponent<BossSpawnSystem>(out BossSpawnSystem bossSpawnSystem))
            {
                _callBossPanel.SetActive(true);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (_bossIsSpawned == false)
        {
            if (other.TryGetComponent<BossSpawnSystem>(out BossSpawnSystem bossSpawnSystem))
            {
                _callBossPanel.SetActive(false);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (_bossIsSpawned == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (other.TryGetComponent<BossSpawnSystem>(out BossSpawnSystem bossSpawnSystem))
                {
                    bossSpawnSystem.SpawnBoss();
                    _callBossPanel.SetActive(false);
                    _bossIsSpawned = true;
                }
            }
        }
    }
}
