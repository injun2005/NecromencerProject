using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Skill
{
    private string effectName = "firball";
    public override void UseSkill(Character skillTarget)
    {
        StartCoroutine(EffectSkill());
        character.Attack(character.AD + 2);
    }

    public IEnumerator EffectSkill()
    {
        GameObject obj = ObjectPool.instance.Pop(effectName);
        obj.transform.position = transform.position;
        yield return new WaitForSeconds(2);
        ObjectPool.instance.Push(effectName, obj);
    }
}
