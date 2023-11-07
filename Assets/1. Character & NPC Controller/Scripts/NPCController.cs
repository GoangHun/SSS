using UnityEngine;
using UnityEngine.AI;

public class NPCController : MonoBehaviour
{

    public enum Status
    {
        Idel,
        Patrol,
        Trace,
    }
    private Status currentStatus;
    public Status CurrStatus
    {
        get { return currentStatus; }
        set 
        { 
            var prevStatus = currentStatus;
            currentStatus = value;

            timer = 0f;
            agent.speed = speed; //�ȱ�
            agent.isStopped = false;
            switch (currentStatus)
            {
                case Status.Idel:
                    agent.isStopped = true;
                    break;
                case Status.Patrol:
                    //waypointIndex = Random.Range(0, waypoints.Length);
                    // ��ġ�� �����ϰ� �����̱� ���ؼ� �۵���Ŵ
                    waypointIndex++;
                    waypointIndex %= waypoints.Length;
                    agent.destination = waypoints[waypointIndex].position;
                    break;
                case Status.Trace:
                    agent.speed = agentSpeed;//�ٱ�
                    agent.destination = player.transform.position;
                    break;
            }
        }
    }
    private float timer = 0f;
    public float idleTime = 1f;
    public float traceInterval = 0.5f;

    public float aggroRange = 10; // distance in scene units below which the NPC will increase speed and seek the player
    public Transform[] waypoints; // collection of waypoints which define a patrol area

    int waypointIndex; // the current waypoint index in the waypoints array
    float speed, agentSpeed; // current agent speed and NavMeshAgent component speed
    Transform player; // reference to the player object transform
    float distansToPlayer;

    Animator animator; // reference to the animator component
    NavMeshAgent agent; // reference to the NavMeshAgent

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (agent != null) 
        { 
            agentSpeed = agent.speed;
            speed = agentSpeed * 0.5f;
        }
        player = GameObject.FindWithTag("Player").transform;
        waypointIndex = 0;
    }
    private void Start()
    {
        currentStatus = Status.Idel;
    }
    private void Update()
    {

         distansToPlayer = Vector3.Distance(transform.position, player.position);

        switch (currentStatus)
        {
            case Status.Idel:
                UpdateIdle();
                break;
            case Status.Patrol:
                UpdatePatrol();
                break;
            case Status.Trace:
                UpdateTrace();
                break;
        }
        animator.SetFloat("Speed", agent.velocity.sqrMagnitude);
    }
    private void OnDrawGizmos()
    {
        var prevColor = Gizmos.color;
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,aggroRange);

        Gizmos.color = prevColor; 
        // ������� ���� ���̰���
    }
    private void UpdateIdle()
    {
        if (distansToPlayer < aggroRange)
        {
            CurrStatus = Status.Trace;
            return;
        }

        timer += Time.deltaTime;
        if(timer > idleTime)
        {
            CurrStatus = Status.Patrol;
        }
    }
    private void UpdatePatrol()
    {
        if (distansToPlayer < aggroRange)
        {
            CurrStatus = Status.Trace;
            return;
        }

        if (agent.remainingDistance < agent.stoppingDistance)  
            // agent.remainingDistance -> ������������ �����ִ� �Ÿ� ǥ��
            // agent.stoppingDistatnce -> ������ �ݰ� �Ÿ��� ��Ÿ���°���
        {
            CurrStatus = Status.Idel;
        }
    }

    private void UpdateTrace()
    {
        if (distansToPlayer>aggroRange)
        {
            CurrStatus = Status.Idel;
            return;
        }
        timer += Time.deltaTime;
        if (timer > traceInterval)
        {
            timer = 0f;
            agent.destination = player.position; //���ʿ��� �۾��� 
            // �ð��� �༭ �۾��� �ϰԵ�
        }
    }
}
