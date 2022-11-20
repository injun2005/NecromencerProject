using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public enum ESkillKeys// 따로 스크립트 빼는게 관리하기 좋을듯
    {
        None,
        Fireball,
    }
    private Character character;

    public int limitMP;
    public int limitLevel;
    public bool isActive { get { return character.MP >= limitMP && character.Level >= limitLevel; } }

    public virtual void UseSkill(Character skillTarget)
    {

    }
}
