using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotSpeedImprovement : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            Destroy(this.gameObject.transform.parent.gameObject);
            if(player.gameObject.transform.Find("Weapon").gameObject.TryGetComponent(out Shooting shooting))
            {
                if (shooting._timeToReload > 0f)
                {
                    shooting._timeToReload -= 0.05f;
                }
            }
        }
    }
}
