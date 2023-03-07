using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : BaseState
{
    //Current targeted waypoint
    public int waypointIndex;
    [SerializeField] private float waitTime;
    private float time;
    public override void Enter()
    {
    
    }
    public override void Perform()
    {
        PatrolCycle();
        Collider[] hits = Physics.OverlapBox(enemy.transform.position, enemy.GetComponent<BoxCollider>().size / 2f, enemy.transform.rotation, enemy.GetDetectionLayer());
        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                Debug.Log("Colliding with " + hit.gameObject.name);
                enemy.SetTarget(hit.gameObject);
                stateMachine.ChangeState(stateMachine.alertState);
            }
    }
    }
    public override void Exit()
    {
        
    }

    public void PatrolCycle()
    {
        if(enemy.Agent.remainingDistance < 0.2f)
        {
            time += Time.deltaTime;
            if(time > waitTime)
            {
                if(waypointIndex < enemy.path.waypoints.Count - 1)
                    waypointIndex++;
                else
                    waypointIndex = 0;

                enemy.Agent.SetDestination(enemy.path.waypoints[waypointIndex].position);
                time = 0;
            }
        }
    }
}
