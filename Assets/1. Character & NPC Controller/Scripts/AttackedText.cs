using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class AttackedText : MonoBehaviour, IAttackable
{
    public Color color = Color.white;
    public AttackedTextEffect prefab;
    public float yOffset = 1.5f;

    public void OnAttack(GameObject attacker, Attack attack)
    {
        var pos = transform.position;
        pos.y += yOffset;

        var textGo = Instantiate(prefab, pos, Quaternion.identity);
        textGo.Set(attack.Damage.ToString(), color);

        
    }
}
