using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Skill
{
    private string effectName = "fireball";
    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("Fireball");
        StartCoroutine(EffectSkill());
        character.Attack(character.AD + 2);
    }

    public IEnumerator EffectSkill()
    {
        GameObject obj = ObjectPool.instance.Pop(effectName);
        obj.transform.position = transform.position;

        Vector3 dir = obj.transform.position - character.Traget.transform.position;
        dir.Normalize();
        obj.transform.rotation = Quaternion.LookRotation(dir);
        Sound.OnPlayEffectSound(Sound.EEffect.Fireball);
        yield return new WaitForSeconds(2);
        ObjectPool.instance.Push(effectName, obj);
        character.isAction = false;
    }
}
