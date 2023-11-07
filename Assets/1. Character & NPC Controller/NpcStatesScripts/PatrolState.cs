using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolState : NpcStateBase
{
    protected float timer = 0f;
    private int waypointIndex = 0;
    public PatrolState(NpcController2 manager) : base(manager)
    {

    }

    public override void Enter()
    {
        waypointIndex++;
        waypointIndex %= waypoints.Length;
        agent.destination = waypoints[waypointIndex].position;
    }

    public override void Exit()
    {
       
    }

    public override void Update()
    {
        var distansToPlayer = Vector3.Distance(player.position, npcController.transform.position);
        if (distansToPlayer < npcController.aggroRange)
        {
            npcController.SetState(NpcController2.States.Trace);
            return;
        }
        //agent.pathPending �� ��θ� ����� �Ϸ� �Ǵ����̸� true
        if (agent.pathPending && (agent.remainingDistance < agent.stoppingDistance))
        {
            npcController.SetState(NpcController2.States.Idle);
        }
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }
}

// ������ ��Ƶδ� �� ���� ���� �߰� ���� �и� 
// ���ӿ�����Ʈ�� �޾Ƽ� �ϱ�
// stateManager�� �����̹� ��ӹ޾Ƽ� �۾�