using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageImprovement : Improvement
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Destroy(this.gameObject.transform.parent.gameObject);
            player.damage += 2;
            base.AddToInventory();
        }
    }
}
