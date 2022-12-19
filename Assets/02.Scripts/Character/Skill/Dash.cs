using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : Skill
{
    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("Dash");
        int addDamage = character.Speed - skillTarget.Speed > 0 ? character.Speed - skillTarget.Speed : 0;

        character.Attack(character.AD + addDamage);
    }
}
