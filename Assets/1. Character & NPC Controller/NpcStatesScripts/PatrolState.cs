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
        //agent.pathPending 은 경로를 계산이 완료 되는중이면 true
        if (agent.pathPending && (agent.remainingDistance < agent.stoppingDistance))
        {
            npcController.SetState(NpcController2.States.Idle);
        }
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }
}

// 정보를 담아두는 놈 따로 상태 추가 따로 분리 
// 게임오브젝트를 받아서 하기
// stateManager을 모노비에이버 상속받아서 작업