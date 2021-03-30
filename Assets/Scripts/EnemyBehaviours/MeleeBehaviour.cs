using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeBehaviour : EnemyBehaviour
{
    public override void Attack()
    {
        base.Attack();
    }
    private void Start()
    {
        GetComponent<Animator>().SetBool("isWalk", true);
    }
    private void Update()
    {
        transform.LookAt(_player.transform);
    }
}
