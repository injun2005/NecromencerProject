using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Posion : Skill
{
    private string effectName = "Posion";

    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("Posion");
        StartCoroutine(EffectSkill());
        character.Attack(character.AD + 3);
    }

    public IEnumerator EffectSkill()
    {
        GameObject obj = ObjectPool.instance.Pop(effectName);
        obj.transform.position = transform.position;
        yield return new WaitForSeconds(2);
        ObjectPool.instance.Push(effectName, obj);
    }
}
