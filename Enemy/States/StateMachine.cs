using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;
    public PatrolState patrolState;
    public AlertState alertState;
    public AttackState attackState;
    
    public void Initialise()
    {
        //Setup default state
        patrolState = new PatrolState();
        alertState = new AlertState();
        attackState = new AttackState();
        ChangeState(patrolState);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(activeState != null)
            activeState.Perform();
    }

    public void ChangeState(BaseState newState)
    {
        //Check active state
        if(activeState != null)
        {
            //Run cleanup on active state
            activeState.Exit();
        }
        
        //Change to new state
        activeState = newState;

        if(activeState != null)
        {
            //Setup new state machine
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            //Assign state enemy class
            activeState.Enter();
        }
    }
}
