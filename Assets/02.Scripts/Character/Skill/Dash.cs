using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Skill
{
    private string effectName = "dash";
    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("Dash");

        StartCoroutine(EffectSkill());
        int addDamage = character.Speed - skillTarget.Speed > 0 ? character.Speed - skillTarget.Speed : 0;
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
