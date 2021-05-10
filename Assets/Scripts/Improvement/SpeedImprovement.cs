using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedImprovement : Improvement
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter player))
        {
            Destroy(this.gameObject.transform.parent.gameObject);
            player.m_MoveSpeedMultiplier += 0.1f;
            player.m_AnimSpeedMultiplier += 0.1f;
            base.AddToInventory();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.TryGetComponent(out UnityStandardAssets.Characters.ThirdPerson.ThirdPersonCharacter player))
        {
            Destroy(this.gameObject);
            player.m_MoveSpeedMultiplier += 0.1f;
            player.m_AnimSpeedMultiplier += 0.1f;
            base.AddToInventory();
        }
    }
}
