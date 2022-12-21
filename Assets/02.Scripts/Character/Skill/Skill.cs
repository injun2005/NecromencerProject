using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ESkillKeys// ���� ��ũ��Ʈ ���°� �����ϱ� ������
{
    None,
    Fireball,
    Dash,
    Boom,

}
public abstract class Skill : MonoBehaviour
{
    public ESkillKeys skillKey;
    protected Character character;

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
    public abstract void UseSkill(Character skillTarget);
}
