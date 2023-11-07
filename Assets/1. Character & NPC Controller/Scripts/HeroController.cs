using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class HeroController : MonoBehaviour
{
    private Inventory inventory;
    Animator animator; // reference to the animator component
    NavMeshAgent agent; // reference to the NavMeshAgent

    private GameObject attackTarget;
    private Coroutine coMoveAndAttack;

    void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        inventory = GetComponent<Inventory>();
    }

    private void Update()
    {
        animator.SetFloat("Speed", agent.velocity.magnitude);
    }

    public void SetDestination(Vector3 destination)
    {
        if (coMoveAndAttack != null)
        {
            StopCoroutine(coMoveAndAttack);
            coMoveAndAttack = null;
        }
        attackTarget = null;
        agent.isStopped = false;
        agent.destination = destination;
    }

    public void AttackTarget(GameObject target)
    {
        if (coMoveAndAttack != null)
        {
            StopCoroutine(coMoveAndAttack);
            coMoveAndAttack = null;
        }
        attackTarget = target;
        coMoveAndAttack = StartCoroutine(CoMoveAndAttack());
    }

    private IEnumerator CoMoveAndAttack()
    {
        var range = 0f;
        if (inventory != null && inventory.CurrentWeapon != null)
        {
            range =  inventory.CurrentWeapon.range;
        }

        agent.isStopped = false;
        var distance = Vector3.Distance(transform.position, attackTarget.transform.position);
        while(distance > range)
        {
            agent.destination = attackTarget.transform.position;
            yield return new WaitForSeconds(0.1f);
            distance = Vector3.Distance(transform.position, attackTarget.transform.position);
        }
        agent.isStopped = true;

        if (inventory != null && inventory.CurrentWeapon != null)
        {
            var lookPos = attackTarget.transform.position;
            lookPos.y = transform.position.y;
            transform.LookAt(lookPos);
            animator.SetTrigger("Attack");
        }
           
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
