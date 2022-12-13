using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESkillKeys// 따로 스크립트 빼는게 관리하기 좋을듯
{
    None,
    Fireball,
    Dash,
}
public class Skill : MonoBehaviour
{
    public ESkillKeys skillKey;
    private Character character;

    public int limitMP;
    public int limitLevel;
    [Multiline]
    public string skillInfo;
    public bool isActive { get { return character.Level >= limitLevel; } }
    public bool isCanUse { get { return character.MP >= limitMP && character.Level > limitLevel; } }
    
    public virtual void Init(Character character)
    {
        this.character = character;
    }    
    public virtual void UseSkill(Character skillTarget)
    {

    }
}
