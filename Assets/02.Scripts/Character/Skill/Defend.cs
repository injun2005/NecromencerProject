using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Defend : Skill
{
    private string effectName = "Defend";

    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("Defend");
        StartCoroutine(EffectSkill());
        int addDamage = character.Speed - skillTarget.Defence > 0 ? character.Defence - skillTarget.Defence : 0;
        character.Attack(character.AD + addDamage);
    }

    public IEnumerator EffectSkill()
    {
        GameObject obj = ObjectPool.instance.Pop(effectName);
        obj.transform.position = transform.position;
        yield return new WaitForSeconds(2);
        ObjectPool.instance.Push(effectName, obj);
    }
}
