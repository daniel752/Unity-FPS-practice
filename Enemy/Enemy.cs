using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private StateMachine stateMachine;
    private NavMeshAgent agent;
    public NavMeshAgent Agent {get => agent;}
    public Path path;
    [SerializeField] private string currentState;
    private GameObject target;
    [SerializeField] private float rotationSpeed = 8;
    // [SerializeField] private float moveSpeed = 3;
    [SerializeField] LayerMask detectionLayer;
    [SerializeField] Weapon weapon;
    [SerializeField] float attackRange = 5;
    [SerializeField] float damage = 2;
    [SerializeField] float timeBetweenAttacks = 3;
    [SerializeField] PlayerExperience playerExperience;
    void Start()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        detectionLayer = LayerMask.GetMask("Player");
        weapon = GetComponent<Weapon>();
        target = null;
        playerExperience = GameObject.FindWithTag("Player").GetComponent<PlayerExperience>();
    }

    // Update is called once per frame
    void Update()
    {
        currentState = stateMachine.activeState.ToString();
    }
    public void RotateToPlayer()
    {
        // Rotate towards the player
        // transform.LookAt(target.transform);
        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
    }
    public void MoveToPlayer()
    {
        //Move towards player
        // Debug.Log(name + " moving to " + target.name);
        agent.SetDestination(target.transform.position);
        // transform.position += transform.TransformDirection(transform.forward * moveSpeed * Time.deltaTime);
    }
    public void Die()
    {
        // Debug.Log($"{this} died");
        playerExperience.GainExp(10);
    }
    public void SetTarget(GameObject target)
    {
        this.target = target;
    }
    public GameObject GetTarget()
    {
        return target;
    }
    public LayerMask GetDetectionLayer()
    {
        return detectionLayer;
    }
    public bool TargetInAttackRange()
    {
        if (Vector3.Distance(transform.position,target.transform.position) <= attackRange)
        {
            // Debug.Log("Target " + target.name + " in range");
            return true;
        }
        // Debug.Log("Target " + target.name + " not in range");
        return false;
    }
    public float GetDamage()
    {
        return damage;
    }
    public float GetTimeBetweenAttacks()
    {
        return timeBetweenAttacks;
    }
}
