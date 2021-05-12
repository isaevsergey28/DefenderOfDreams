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
