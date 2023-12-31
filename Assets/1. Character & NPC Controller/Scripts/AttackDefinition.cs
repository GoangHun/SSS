using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Attack.asset", menuName = "Attack/BaseAttack")] //어트리뷰트는 전부 System.Attribute를 상속받은 클래스
public class AttackDefinition : ScriptableObject    //Asset으로 취급
{
    public float cooldown;
    public float range;
    public float minDamage;
    public float maxDamage;
    public float criticalChance;    //0.0 ~ 1.0
    public float criticalMultiplier;

    public Attack CreateAttack(CharacterStats attacker, CharacterStats defender)
    {
        float damage = attacker.damage;
        damage += Random.Range(minDamage, maxDamage);

        bool critical = Random.value < criticalChance;
        if (critical)
        {
            damage *= criticalMultiplier;
        }

        if (defender != null)
        {
            damage -= defender.armor;
        }
        return new Attack((int)damage, critical);
    }
}
