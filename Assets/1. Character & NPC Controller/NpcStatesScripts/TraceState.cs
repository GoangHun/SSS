using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraceState : NpcStateBase
{
    protected float timer = 0f;

    public TraceState(NpcController2 manager) : base(manager)
    {

    }

    public override void Enter()
    {
        agent.speed = agentSpeed;
        agent.destination = player.transform.position;
    }

    public override void Exit()
    {
  
    }

    public override void Update()
    {
        var distansToPlayer = Vector3.Distance(player.position, npcController.transform.position);
        if (distansToPlayer > npcController.aggroRange)
        {
            npcController.SetState(NpcController2.States.Idle);
            return;
        }
        else if (distansToPlayer <= npcController.attackRange)
        {
            npcController.SetState(NpcController2.States.Attack);
            return;
        }
        timer += Time.deltaTime;
        if (timer > npcController.traceInterval)
        {
            timer = 0f;
            agent.destination = player.position; 
        }
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }
}

