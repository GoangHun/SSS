using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : NpcStateBase
{
    private bool isAttacking = false;
    protected float timer = 0f;

    public AttackState(NpcController2 manager) : base(manager)
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
        if (distansToPlayer > npcController.attackRange)
        {
            npcController.SetState(NpcController2.States.Trace);
            return;
        }

        isAttacking = true;
        animator.SetTrigger("Attack");
    }
}
