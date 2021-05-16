using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedImprovement : Improvement
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out RelativeMovement player))
        {
            Destroy(this.gameObject);
            player.moveSpeed++;
            _animator.SetFloat("MoveSpeed", (int)(_animator.GetFloat("MoveSpeed") + 0.1f));
            base.AddToInventory();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out RelativeMovement player))
        {
            Destroy(this.gameObject);
            base.AddToInventory();
        }
    }
}
