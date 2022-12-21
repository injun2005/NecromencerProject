using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;


public class Boom : Skill
{
    public EffectPrefab SkillEffect;

    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("bomb");
        StartCoroutine(EffectSkill());
        character.Attack(character.AD + 3);
    }

    public IEnumerator EffectSkill()
    {
        EffectPrefab effectPrefab = Instantiate(SkillEffect, character.transform);
        effectPrefab.EffectStart();
        yield return new WaitForSeconds(2f);
    }
}
