using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NpcController2 : MonoBehaviour
{
    //stateManager 클래스를 이용한 몬스터 상태 패턴 움직임
    public enum States
    {
        Idle,
        Patrol,
        Trace,
        Attack,
    }

    private StateManager stateManager = new StateManager();
    private List<StateBase> states = new List<StateBase>();

    public float idleTime = 1f;
    public float traceInterval = 0.5f;
    public float aggroRange = 10;
    public float attackRange = 1f;
    public Transform[] waypoints;
    [HideInInspector]
    public Inventory inventory;
    [HideInInspector] 
    public GameObject attackTarget;

    Animator animator; 
    NavMeshAgent agent; 

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        inventory = GetComponent<Inventory>();
        attackTarget = GameObject.FindWithTag("Player");
    }
    private void Start()
    {
        states.Add(new IdleState(this));
        states.Add(new PatrolState(this));
        states.Add(new TraceState(this));
        states.Add(new AttackState(this));


        SetState(States.Idle);
    }

    public void SetState(States newState)
    {
        stateManager.ChangeState(states[(int)newState]);
    }

    void Update()
    {
        stateManager.Update();
    }

    private void Hit()
    {
        if (inventory == null || inventory.CurrentWeapon == null)
        { return; }

        if (inventory.CurrentWeapon is Weapon)
        {
            var weapon = (Weapon)inventory.CurrentWeapon;
            weapon.ExecuteAttack(gameObject, attackTarget);
        }
    }
}
