using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeSwordSystem : MonoBehaviour
{
    private MeleeBehaviour _meleeBehaviour;

    [SerializeField] private float _swordDamage = 5f;

    private void Start()
    {
        _meleeBehaviour = gameObject.transform.parent.transform.parent.GetComponent<MeleeBehaviour>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(_meleeBehaviour.isHitActive)
        {
            if (other.gameObject.TryGetComponent(out Player player))
            {
                player.GiveDamage(_swordDamage);
            }
        }
        
    }
}
