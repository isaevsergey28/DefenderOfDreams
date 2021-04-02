﻿using UnityEngine;
using System.Collections;

public class Arrive : AgentBehaviour
{
    public float targetRadius;
    public float slowRadius;
    public float timeToTarget = 0.1f;
    public bool isTimeToAttack { get; private set; } = false;
    public bool isStopped = false;

    public override Steering GetSteering()
    {
        
        Steering steering = new Steering();
        Vector3 direction = _target.transform.position - transform.position;
        float distance = direction.magnitude;
        float targetSpeed;
        if (distance < targetRadius || isStopped)
        {
            isTimeToAttack = true;
            return steering;
        }
        if (distance > slowRadius)
            targetSpeed = agent.maxSpeed;
        else
            targetSpeed = agent.maxSpeed * distance / slowRadius;
        Vector3 desiredVelocity = direction;
        desiredVelocity.Normalize();
        desiredVelocity *= targetSpeed;
        steering.linear = desiredVelocity - agent.velocity;
        steering.linear /= timeToTarget;
        if (steering.linear.magnitude > agent.maxAccel)
        {
            steering.linear.Normalize();
            steering.linear *= agent.maxAccel;
        }
        return steering;
    }

    public void CheckDistance()
    {
        Vector3 direction = _target.transform.position - transform.position;
        float distance = direction.magnitude;
        if (distance > targetRadius)
        {
            isStopped = false;
            isTimeToAttack = false;
        }
    }
}
