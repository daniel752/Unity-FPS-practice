using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : BaseState
{
    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        if (enemy.TargetInAttackRange())
        {
            // Debug.Log("Target " + enemy.GetTarget().name + " in range");
            enemy.GetEnemyAim().AimAtTarget(enemy.GetTarget().transform);
            enemy.RotateToPlayer();
            enemy.AddTime(Time.deltaTime);
            // Debug.Log($"time {time} >=? time between attack {enemy.GetTimeBetweenAttacks()}");
            if (enemy.GetTime() >= enemy.GetTimeBetweenAttacks())
            {
                // Debug.Log($"{this} attack");
                Attack();
            }
        }
        else
        {
            Debug.Log("Target " + enemy.GetTarget().name + " out of range");
            enemy.GetEnemyWeapon().firing = false;
            stateMachine.ChangeState(stateMachine.alertState);
        }
    }
    public override void Exit()
    {
        
    }
    private void Attack()
    {
        // Debug.Log($"attack time {enemy.GetTime()}");
        enemy.AttackMovement();
        enemy.GetEnemyWeapon().firing = true;
        enemy.GetEnemyWeapon().Fire();
        enemy.SetTime(0);
        // PlayerHealth target = enemy.GetTarget().GetComponent<PlayerHealth>();
        // target.TakeDamage(enemy.GetDamage());
    }
}
