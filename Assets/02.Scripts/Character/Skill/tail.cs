using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class tail : Skill
{
    private string effectName = "Tail";

    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("Tail");
        StartCoroutine(EffectSkill());
        character.Attack(character.AD + 2);
    }

    public IEnumerator EffectSkill()
    {
        GameObject obj = ObjectPool.instance.Pop(effectName);
        obj.transform.position = transform.position;
        character.transform.rotation = Quaternion.Euler(0, 0, 0);
        yield return new WaitForSeconds(2);
        character.transform.rotation = Quaternion.Euler(0, -180, 0);
        ObjectPool.instance.Push(effectName, obj);
    }
}
