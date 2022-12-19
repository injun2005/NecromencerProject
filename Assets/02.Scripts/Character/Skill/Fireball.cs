using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : Skill
{
    public override void UseSkill(Character skillTarget)
    {
        Debug.Log("Fireball");
        character.Attack(character.AD + 2);
    }
}
