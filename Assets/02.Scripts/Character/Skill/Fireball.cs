using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Skill
{
    public GameObject SkillEffect;
    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("Fireball");
        StartCoroutine(EffectSkill());
        character.Attack(character.AD + 2);
    }

    public IEnumerator EffectSkill()
    {
        GameObject GO = Instantiate(SkillEffect,character.transform);
        yield return new WaitForSeconds(2f);
        Destroy(GO);
    }
}
