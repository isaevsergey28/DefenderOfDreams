using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageImprovement : Improvement
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Destroy(this.gameObject);
            player.damage += 2;
            base.AddToInventory();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Destroy(this.gameObject);
            player.damage += 2;
            base.AddToInventory();
        }
    }
}
