﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyBehaviour
{
    void Attack();
    void Destroy(GameObject enemy, float time);
}
