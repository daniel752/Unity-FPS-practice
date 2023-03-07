using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlertState : BaseState
{
    public override void Enter()
    {
        
    }
    public override void Perform()
    {
        if (enemy.GetTarget() != null && !enemy.TargetInAttackRange())
        {
            enemy.RotateToPlayer();
            enemy.MoveToPlayer();
        }
        if (enemy.TargetInAttackRange())
        {
            stateMachine.ChangeState(stateMachine.attackState);
        }
    }
    public override void Exit()
    {
        
    }

}
