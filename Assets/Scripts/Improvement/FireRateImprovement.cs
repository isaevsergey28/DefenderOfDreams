using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateImprovement : Improvement
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Destroy(this.gameObject);
            if(player.gameObject.transform.Find("Weapon").gameObject.TryGetComponent(out Shooting shooting))
            {
                if (shooting._timeToReload > 0f)
                {
                    shooting._timeToReload -= 0.05f;
                }
                base.AddToInventory();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Destroy(this.gameObject);
            if(player.gameObject.transform.Find("Weapon").gameObject.TryGetComponent(out Shooting shooting))
            {
                if (shooting._timeToReload > 0f)
                {
                    shooting._timeToReload -= 0.05f;
                }
                base.AddToInventory();
            }
        }
    }
}
