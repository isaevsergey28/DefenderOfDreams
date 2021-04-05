using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ArrowBehaviour : MonoBehaviour
{
    private Player _player;
    private Vector3 _playerPos;
    [SerializeField] private float _arrowSpeed = 10f;
    [SerializeField] private float _arrowDamage = 10f;
    [Inject]
    private void Construct(Player player)
    {
        _player = player;
    }
    private void Start()
    {
        // _playerPos = _player.gameObject.transform.position;
        _playerPos = GameObject.Find("Player").transform.position;
       
    }
   private void Update()
   {
        transform.position =  Vector3.MoveTowards(transform.position, _playerPos, Time.deltaTime * _arrowSpeed);
   }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            player.GiveDamage(_arrowDamage);
        }
    }
}
 