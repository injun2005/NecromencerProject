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
        Sound.OnPlayEffectSound(Sound.EEffect.Dash);
        yield return new WaitForSeconds(2f);
        ObjectPool.instance.Push(effectName, obj);
        character.isAction = true;
    }
}
