using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon.asset", menuName = "Attack/Weapon")]
public class Weapon : AttackDefinition
{
    public GameObject prefab;
    
    public void ExecuteAttack(GameObject attacker, GameObject defender)
    {
        //애니메이션 -> 타격
        if (defender == null)
            return;
        //거리
        if (Vector3.Distance(attacker.transform.position, defender.transform.position) > range)
            return;

        //방향
        var dir = defender.transform.position - attacker.transform.position;
        dir.Normalize();
        var dot = Vector3.Dot(dir, attacker.transform.forward); //내적
        if (dot < 0.5f)
            return;

        var aStats = attacker.GetComponent<CharacterStats>();
        var dStats = defender.GetComponent<CharacterStats>();
        var attack = CreateAttack(aStats, dStats);

        var attackables = defender.GetComponents<IAttackable>();
        foreach (var attackable in attackables)
        {
            attackable.OnAttack(attacker, attack);
        }

    }
}
