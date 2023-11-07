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
    }

    private StateManager stateManager = new StateManager();
    
    private List<StateBase> states = new List<StateBase>();


    public float idleTime = 1f;
    public float traceInterval = 0.5f;

    public float aggroRange = 10; 
    public Transform[] waypoints; 

 

    Animator animator; 
    NavMeshAgent agent; 

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        states.Add(new IdleState(this));
        states.Add(new PatrolState(this));
        states.Add(new TraceState(this));
        
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
}
