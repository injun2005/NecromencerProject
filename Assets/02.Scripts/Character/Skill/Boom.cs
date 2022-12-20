using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : Skill
{
    public GameObject SkillEffect;
    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("bomb");
        StartCoroutine(EffectSkill());
        character.Attack(character.AD + 3);
    }

    public IEnumerator EffectSkill()
    {
        GameObject GO = (GameObject)Instantiate(SkillEffect, character.transform);
        yield return new WaitForSeconds(2f);
        Destroy(GO);
    }
}
