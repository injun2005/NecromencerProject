using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Skill
{
    public GameObject SkillEffect;
    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("Dash");
        StartCoroutine(EffectSkill());
        int addDamage = character.Speed - skillTarget.Speed > 0 ? character.Speed - skillTarget.Speed : 0;
        character.Attack(character.AD + addDamage);
    }

    public IEnumerator EffectSkill()
    {
        GameObject GO = (GameObject)Instantiate(SkillEffect, character.transform);
        yield return new WaitForSeconds(2f);
        Destroy(GO);
    }
}
