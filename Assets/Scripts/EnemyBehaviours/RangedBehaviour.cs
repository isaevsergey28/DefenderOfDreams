using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedBehaviour : EnemyBehaviour
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
        Debug.Log(GameObject.Find("Player").gameObject);
        transform.LookAt(GameObject.Find("Player").gameObject.transform);
    }
}
