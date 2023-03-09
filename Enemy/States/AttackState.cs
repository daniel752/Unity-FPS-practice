using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    private float time = 0;

    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        if (enemy.TargetInAttackRange())
        {
            // Debug.Log("Target " + enemy.GetTarget().name + " in range");
            time += Time.deltaTime;
            if (time >= enemy.GetTimeBetweenAttacks())
                Attack();
        }
        else
        {
            // Debug.Log("Target " + enemy.GetTarget().name + " out of range");
            stateMachine.ChangeState(stateMachine.alertState);
        }
    }
    public override void Exit()
    {
        
    }
    private void Attack()
    {
        time = 0;
        PlayerHealth target = enemy.GetTarget().GetComponent<PlayerHealth>();
        target.TakeDamage(enemy.GetDamage());
    }
}
