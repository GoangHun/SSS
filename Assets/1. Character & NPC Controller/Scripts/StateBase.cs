using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class StateBase
{
    abstract public void Enter();
    abstract public void Update();
    abstract public void Exit();
}

public abstract class NpcStateBase : StateBase
{
    protected NpcController2 npcController;
    protected Animator animator;
    protected NavMeshAgent agent;
    protected float agentSpeed, speed;
    protected Transform player;
    protected Transform[] waypoints;
    protected CharacterStats characterStats;

    public NpcStateBase(NpcController2 npcCtrl)
    {
        npcController = npcCtrl;    
        animator = npcController.GetComponent<Animator>();
        agent = npcController.GetComponent<NavMeshAgent>();
        characterStats = npcController.GetComponent<CharacterStats>();

        agentSpeed = agent.speed;
        speed = agentSpeed * 0.5f;

  
        player = GameObject.FindWithTag("Player").transform;
        waypoints = npcController.waypoints;
    }
}