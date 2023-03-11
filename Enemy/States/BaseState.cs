using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseState
{
    //Instance of enemy class
    public Enemy enemy;
    //Instance of state-machine class
    public StateMachine stateMachine;
    protected NavMeshAgent navMeshAgent;
    

    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();

    public NavMeshAgent GetNavMeshAgent()
    {
        return navMeshAgent;
    }
    public void SetNavMeshAgent(NavMeshAgent navMeshAgent)
    {
        this.navMeshAgent = navMeshAgent;
    }
}
