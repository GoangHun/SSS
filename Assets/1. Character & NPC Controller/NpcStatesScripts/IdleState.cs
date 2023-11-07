using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleState : NpcStateBase
{

    protected float timer = 0f;

    public IdleState(NpcController2 manager) : base(manager)
    {

    }

    public override void Enter()
    {
        timer = 0f;

        agent.speed = speed;
        agent.isStopped = true;
    }

    public override void Exit()
    {
        agent.isStopped = false;
    }

    public override void Update()
    {
        var distansToPlayer = Vector3.Distance(player.position, npcController.transform.position);
        if (distansToPlayer < npcController.aggroRange)
        {
            npcController.SetState(NpcController2.States.Trace);
            return;
        }

        timer += Time.deltaTime;
        if (timer > npcController.idleTime)
        {
            npcController.SetState(NpcController2.States.Patrol);

        }
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }
}
