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
    [SerializeField] EnemyWeapon weapon;
    [SerializeField] float attackRange = 5;
    [SerializeField] float damage = 2;
    [SerializeField] float timeBetweenAttacks = 1.5f;
    [SerializeField] float moveRadius = 5f;
    float time = 0;
    // [SerializeField] float moveTime = 5f;
    PlayerExperience playerExperience;
    EnemyAim enemyAim;
    void Awake()
    {
        stateMachine = GetComponent<StateMachine>();
        agent = GetComponent<NavMeshAgent>();
        stateMachine.Initialise();
        weapon.Init();
        detectionLayer = LayerMask.GetMask("Player");
        // weapon = GetComponent<Weapon>();
        target = null;
        playerExperience = GameObject.FindWithTag("Player").GetComponent<PlayerExperience>();
        enemyAim = GetComponentInChildren<EnemyAim>();
    }
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
    public void AttackMovement()
    {
        Vector3 randomDirection = UnityEngine.Random.insideUnitSphere * moveRadius;
        Vector3 destination = target.transform.position + randomDirection;
        agent.SetDestination(destination);
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
        if (weapon != null)
        {
            // Debug.Log($"{weapon} isn't null");
            // Projectile projectile = weapon.GetProjectilePrefab().GetComponent<Projectile>();
            // Debug.Log($"projectile range {projectile.GetRange()}");
            // Debug.Log($"distance:{Vector3.Distance(transform.position,target.transform.position)} <=? {weapon.GetRange()}");
            if (Vector3.Distance(transform.position,target.transform.position) <= weapon.GetRange())
            {
                Debug.Log($"{target} in weapon {weapon} range of {weapon.GetRange()}");
                return true;
            }
            else
                return false;
        }
        else if (Vector3.Distance(transform.position,target.transform.position) <= attackRange)
        {
            Debug.Log("Target " + target + " in range");
            return true;
        }
        Debug.Log("Target " + target.name + " not in range");
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
    public EnemyWeapon GetEnemyWeapon()
    {
        return weapon;
    }
    public float GetTime()
    {
        return time;
    }
    public void SetTime(float time)
    {
        this.time = time;
    }
    public void AddTime(float time)
    {
        this.time += time;
    }
    public EnemyAim GetEnemyAim()
    {
        return enemyAim;
    }
}
