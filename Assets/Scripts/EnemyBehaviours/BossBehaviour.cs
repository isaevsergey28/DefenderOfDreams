using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private void Start()
    {
        transform.parent = GameObject.Find("AllEnemies").transform;
    }
}
